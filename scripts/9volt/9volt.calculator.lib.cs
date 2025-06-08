// SPDX-License-Identifier: MPL-2.0

using Holo.Scripting;
using Holo.Scripting.Models;

public class Calculator : HoloLibrary
{
    /// <summary>
    /// Add two numbers together
    /// </summary>
    /// <param name="a">The first number</param>
    /// <param name="b">The second number</param>
    /// <returns></returns>
    public static int Add(int a, int b)
    {
        return a + b;
    }

    /// <summary>
    /// Multiply two numbers together
    /// </summary>
    /// <param name="a">The first number</param>
    /// <param name="b">The second number</param>
    /// <returns></returns>
    public static int Multiply(int a, int b)
    {
        return a * b;
    }

    public Calculator()
        : base(
            new ModuleInfo
            {
                DisplayName = "CalculatorLib",
                QualifiedName = "9volt.calculator",
                Description = "A basic library for testing",
                Author = "9volt",
                Version = 0.1m,
            }
        )
    {
        Logger.Info("Initialized test library!");
    }
}
