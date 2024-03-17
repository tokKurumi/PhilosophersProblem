namespace PhilosophersProblem;

/// <summary>
/// Represents a fork used in the philosophers problem.
/// </summary>
public class Fork(int number)
{
    /// <summary>
    /// Gets the number of the fork.
    /// </summary>
    public int Number { get; } = number;
}
