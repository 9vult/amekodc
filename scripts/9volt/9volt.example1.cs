using System.Threading.Tasks;

using Holo;
using AssCS;

using MsBox.Avalonia;
using MsBox.Avalonia.Enums;

public class Example1 : HoloScript
{	
	/// <summary>
	/// This function is required for execution
	/// </summary>
	/// <returns>Result of the execution</returns>
	public async override Task<ExecutionResult> Execute()
	{
		Logger.Info("Starting...");
		var currentFile = HoloContext.Instance.Workspace.WorkingFile;
		var stripped = currentFile.SelectedEvent.GetStrippedText();
		
		Logger.Info("Displaying the message box");
		var box = MessageBoxManager.GetMessageBoxStandard(
			$"{Name} Cool Message Box!", 
			$"The stripped content of the selected line is:\n{stripped}",
			ButtonEnum.YesNo
		);
		var result = await box.ShowAsync();
		
		switch (result)
		{
			case ButtonResult.Yes:
				Logger.Info("The YES button was clicked!");
				return new ExecutionResult { Status = ExecutionStatus.Success };
			case ButtonResult.No:
				Logger.Error("The NO button was clicked!");
				return new ExecutionResult { Status = ExecutionStatus.Warning };
			default:
				Logger.Error("The X button was clicked!!!");
				return new ExecutionResult { Status = ExecutionStatus.Failure, Message = "An error occured" };
		}
	}
	
	/// <summary>
	/// This function is only needed for execution of exposed functions
	/// </summary>
	/// <param name="qname">Name of the function to execute</param>
	/// <returns></returns>
	public async override Task<ExecutionResult> Execute(string qname)
	{
		Logger.Info($"Function {qname} selected");
		switch (qname)
		{
			case "9volt.example1.math":
				return await Math();
			default:
				return new ExecutionResult { Status = ExecutionStatus.Failure, Message = "Unknown function" };
		}
	}

	/// <summary>
	/// An exposed function
	/// </summary>
	/// <returns>Execution result</returns>
	private async Task<ExecutionResult> Math() {
		var box = MessageBoxManager.GetMessageBoxStandard(
			"Math Prompt", 
			"Did you know that 2+2=4?",
			ButtonEnum.Ok
		);
		var result = await box.ShowAsync();
		return new ExecutionResult { Status = ExecutionStatus.Success };
	}
	
	private static string _name = "Example Script 1";
	private static string _qname = "9volt.example1";
	private static string _desc = "Example script";
	private static string _author = "9volt";
	private static double _ver = 1.0;
	private static LogDisplay _logDisplay = LogDisplay.OnError;
	private static string[]? _expFuncNames = { "9volt.example1.math" }; // can be null or empty
	private static string? _menu = "Example Scripts"; // optional
	public Example1() : base(_name, _qname, _desc, _author, _ver, _logDisplay, _expFuncNames, _menu)
	{ }
}