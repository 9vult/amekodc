// SPDX-License-Identifier: MIT

using AssCS;
using Holo;
using Holo.Scripting;

/// <summary>
/// An example script for testing purposes
/// </summary>
public class Example1 : HoloScript
{
    /// <summary>
    /// Information about the script
    /// </summary>
    public override ScriptInfo Info { get; init; } = new()
    {
        DisplayName = "Example Script 1",
        QualifiedName = "9volt.example1",
        Description = "An example script for testing purposes",
        Author = "9volt",
        Version = 0.1m,
        Exports = [ "9volt.example1.math" ],
        LogDisplay = LogDisplay.Ephemeral
    };

    /// <summary>
    /// Default execution entry point 
    /// </summary>
    /// <returns>An appropriate <see cref="ExecutionResult"/></returns>
    public override async Task<ExecutionResult> ExecuteAsync ()
    {
        Logger.Info($"Example1 executed!");
        return ExecutionResult.Success;
    }
    
    /// <summary>
    /// Entry point for calling exposed functions
    /// </summary>
    /// <param name="methodName">Qualified name of the function to execute</param>
    /// <returns>An appropriate <see cref="ExecutionResult"/></returns>
    public override async Task<ExecutionResult> ExecuteAsync (string methodName)
    {
        Logger.Info($"Example1 executed for function {methodName}");

        switch (methodName)
        {
            case "9volt.example1.math":
                return await Math();
            default:
                return new ExecutionResult { Status = ExecutionStatus.Failure, Message = "Unknown method." };
        }
    }

    /// <summary>
    /// An example exposed function
    /// </summary>
    /// <returns><see cref="ExecutionResult.Success"/> if the math was successful</returns>
    private async Task<ExecutionResult> Math ()
    {
        Logger.Info($"Math executed: 1 + 1 = 2");
        return ExecutionResult.Success;
    }
}
