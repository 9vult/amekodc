// SPDX-License-Identifier: MPL-2.0

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Ameko.Services;
using AssCS;
using AssCS.History;
using Holo.Models;
using Holo.Providers;
using Holo.Scripting;
using Holo.Scripting.Models;
using Material.Icons;

public class AutoSwapper() : HoloScript(ScriptInfo)
{
    private static readonly ModuleInfo ScriptInfo = new()
    {
        DisplayName = "AutoSwapper",
        QualifiedName = "9volt.autoSwapper",
        Headless = true,
        Exports =
        [
            new MethodInfo
            {
                DisplayName = "Swap Honorifics",
                QualifiedName = "honorifics",
                Submenu = "AutoSwapper",
            },
            new MethodInfo
            {
                DisplayName = "Swap Mahjong Terms",
                QualifiedName = "mahjong",
                Submenu = "AutoSwapper",
            },
            new MethodInfo
            {
                DisplayName = "Swap Verbal Tics",
                QualifiedName = "verbalTics",
                Submenu = "AutoSwapper",
            },
            new MethodInfo
            {
                DisplayName = "Configuration",
                QualifiedName = "config",
                Submenu = "AutoSwapper",
            },
        ],
    };

    private readonly IProjectProvider _prjProvider = ScriptServiceLocator.Get<IProjectProvider>();
    private readonly IScriptConfigurationService _config =
        ScriptServiceLocator.Get<IScriptConfigurationService>();
    private readonly IMessageBoxService _msgBoxSvc = ScriptServiceLocator.Get<IMessageBoxService>();

    public override async Task<ExecutionResult> ExecuteAsync(string? methodName)
    {
        if (string.IsNullOrEmpty(methodName))
            return ExecutionResult.Failure;

        if (methodName == "config")
        {
            await SetConfiguration();
            return ExecutionResult.Success;
        }

        return Swap(
            methodName switch
            {
                "honorifics" => '*',
                "mahjong" => '/',
                "verbalTics" => '<',
            }
        )
            ? ExecutionResult.Success
            : NoDocumentLoaded;
    }

    /// <summary>
    /// Swap according to the flag
    /// </summary>
    /// <param name="flag">Flag to use for swapping</param>
    /// <returns><see langword="true"/> if successful</returns>
    private bool Swap(char flag)
    {
        var wsp = _prjProvider.Current.WorkingSpace;
        if (wsp is null)
            return false;
        var doc = wsp.Document;

        // Build style patterns
        var styles = Styles
            .Select(s => new Regex(
                "^" + Regex.Escape(s).Replace("\\*", ".*").Replace("\\?", "."),
                RegexOptions.IgnoreCase
            ))
            .ToList();

        List<Event> changedEvents = [];

        // Start a transaction on the whole file (lol)
        doc.HistoryManager.BeginTransaction(doc.EventManager.Events);

        foreach (var @event in doc.EventManager.Events)
        {
            var changed = false;

            // Toggle comment status of ***-tagged events
            if (@event.Effect == "***")
            {
                @event.IsComment = !@event.IsComment;
                changedEvents.Add(@event);
                changed = true;
            }

            // For text changes, skip anything not in the approved styles list
            if (!styles.Any(rgx => rgx.IsMatch(@event.Style)))
                continue;

            // Convert "{*}foo{*bar}" to "{*}bar{*foo}"
            var regex = new Regex($@"\{{\{flag}\}}([^\{{]+)\{{\{flag}([^\}}]*)\}}");
            if (regex.IsMatch(@event.Text))
            {
                @event.Text = regex.Replace(@event.Text, $@"{{{flag}}}$2{{{flag}$1}}");
                changedEvents.Add(@event);
                if (!changed)
                {
                    changedEvents.Add(@event);
                    changed = true;
                }
            }

            // Convert "foo{**-bar}" to "foo{*}-bar{*}"
            regex = new Regex($@"\{{\{flag}\{flag}([^\}}]*)\}}");
            if (regex.IsMatch(@event.Text))
            {
                @event.Text = regex.Replace(@event.Text, $@"{{{flag}}}$1{{{flag}}}");
                if (!changed)
                {
                    changedEvents.Add(@event);
                    changed = true;
                }
            }

            // Convert "foo{*}{*-bar}" to "foo{**-bar}"
            regex = new Regex($@"\{{\{flag}\}}\{{\{flag}");
            if (regex.IsMatch(@event.Text))
            {
                @event.Text = regex.Replace(@event.Text, $@"{{{flag}{flag}");
                if (!changed)
                    changedEvents.Add(@event);
            }
        }

        wsp.Commit(changedEvents, ChangeType.Modify);
        return true;
    }

    private async Task SetConfiguration()
    {
        var content = string.Join(";", Styles.Select(s => s.Trim()));

        var result = await _msgBoxSvc.ShowInputAsync(
            "AutoSwapper Config",
            "List of styles to swap, separated by semicolons. Wildcards (*, ?) are permitted.",
            content,
            MsgBoxButtonSet.OkCancel,
            MsgBoxButton.Ok
        );

        if (result is null)
            return;

        var (boxResult, userInput) = result.Value;

        if (boxResult == MsgBoxButton.Ok)
            Styles = userInput.Split(';');
    }

    /// <summary>
    /// Styles to use for swapping
    /// </summary>
    private string[] Styles
    {
        get
        {
            if (!_config.TryGet(this, "styles", out string? styles))
            {
                styles = string.Join(";", DefaultStyles);
                _config.Set(this, "styles", styles);
            }

            return styles!.Split(';');
        }
        set => _config.Set(this, "styles", string.Join(";", value.Select(s => s.Trim())));
    }

    /// <summary>
    /// Default styles. * and ? wildcards are permitted
    /// </summary>
    private static readonly string[] DefaultStyles =
    [
        "*Default*",
        "*Main*",
        "*Alt*",
        "*Top*",
        "*Italics*",
        "*Overlap*",
        "*Dialogue*",
        "*Flashback*",
        "*Thoughts*",
    ];

    private static readonly ExecutionResult NoDocumentLoaded = new()
    {
        Status = ExecutionStatus.Failure,
        Message = "No document loaded",
    };
}
