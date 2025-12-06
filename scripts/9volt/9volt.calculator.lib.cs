// SPDX-License-Identifier: MPL-2.0

using Holo.Scripting;
using Holo.Scripting.Models;
using Microsoft.Extensions.Logging;

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

    /// <inheritdoc />
    public Calculator()
        : base(
            new PackageInfo { DisplayName = "CalculatorLib", QualifiedName = "9volt.calculator" }
        )
    {
        Logger.LogInformation("Initialized test library!");
    }
}
