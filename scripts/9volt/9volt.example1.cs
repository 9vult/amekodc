// SPDX-License-Identifier: MPL-2.0

using System.Threading.Tasks;
using Holo.Scripting;
using Holo.Scripting.Models;

//css_include 9volt.calculator.lib.cs;

public class Example1 : HoloScript
{
    public override async Task<ExecutionResult> ExecuteAsync()
    {
        Logger.Info($"This is the primary execution entry point!");
        return ExecutionResult.Success;
    }

    public override async Task<ExecutionResult> ExecuteAsync(string methodName)
    {
        switch (methodName)
        {
            case "add":
                Logger.Info($"The answer to your question is {Calculator.Add(40, 1)}");
                break;
            case "multiply":
                Logger.Info($"The answer to your question is {Calculator.Multiply(17, 2)}");
                break;
            default:
                break;
        }
        return ExecutionResult.Success;
    }

    public Test()
        : base(
            new ModuleInfo
            {
                DisplayName = "Example Script 1",
                QualifiedName = "9volt.example1",
                Description = "An example script for testing purposes",
                Author = "9volt",
                Version = 0.1m,
                Exports =
                [
                    new MethodInfo
                    {
                        DisplayName = "Addition!",
                        QualifiedName = "add",
                        Submenu = "Math",
                    },
                    new MethodInfo
                    {
                        DisplayName = "Multiplication!",
                        QualifiedName = "multiply",
                        Submenu = "Math",
                    },
                ],
                LogDisplay = LogDisplay.OnError,
            }
        )
    {
        Logger.Info("Initialized test script!");
    }
}
