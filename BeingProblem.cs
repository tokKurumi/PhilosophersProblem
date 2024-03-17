namespace PhilosophersProblem;

/// <summary>
/// Represents a class that solves the dining philosophers problem.
/// </summary>
public class BeingProblem
{
    private readonly int _count;
    private readonly IReadOnlyList<Fork> _forks;
    private readonly IReadOnlyList<Philosopher> _philosophers;

    /// <summary>
    /// Initializes a new instance of the <see cref="BeingProblem"/> class.
    /// </summary>
    /// <param name="philosophersCount">The number of philosophers.</param>
    public BeingProblem(int philosophersCount)
    {
        _count = philosophersCount;

        _forks = new List<Fork>(Enumerable.Range(0, _count)
            .Select(fn => new Fork(fn)));

        _philosophers = new List<Philosopher>(Enumerable.Range(0, _count)
            .Select(pn => new Philosopher(pn, _forks[pn], _forks[(pn + 1) % _count])));
    }

    /// <summary>
    /// Solves the dining philosophers problem asynchronously.
    /// </summary>
    /// <returns>A task representing the asynchronous solving operation.</returns>
    public async Task SolveAsync()
    {
        var solvingTasks = Enumerable.Range(0, _count)
            .Select(tn => _philosophers[tn].LiveAsync());

        await Task.WhenAll(solvingTasks);
    }
}
