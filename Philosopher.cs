namespace PhilosophersProblem;

/// <summary>
/// Represents a philosopher in the dining philosophers problem.
/// </summary>
/// <remarks>
/// Initializes a new instance of the <see cref="Philosopher"/> class.
/// </remarks>
/// <param name="number">The number of the philosopher.</param>
/// <param name="leftFork">The left fork.</param>
/// <param name="rightFork">The right fork.</param>
public class Philosopher(int number, Fork leftFork, Fork rightFork)
{
    /// <summary>
    /// Gets the number of the philosopher.
    /// </summary>
    public int Number { get; } = number;

    /// <summary>
    /// Gets the left fork.
    /// </summary>
    public Fork LeftFork { get; } = leftFork;

    /// <summary>
    /// Gets the right fork.
    /// </summary>
    public Fork RightFork { get; } = rightFork;

    /// <summary>
    /// Gets the status of the philosopher.
    /// </summary>
    public PhilosopherStatus Status { get; private set; } = PhilosopherStatus.Hungry;

    /// <summary>
    /// Makes the philosopher eat.
    /// </summary>
    public void Eat()
    {
        if (Status is PhilosopherStatus.Full)
        {
            return;
        }

        bool leftForkTaken = false;
        bool rightForkTaken = false;

        try
        {
            Monitor.Enter(LeftFork, ref leftForkTaken);
            Monitor.Enter(RightFork, ref rightForkTaken);

            Console.WriteLine($"😋 Philosopher[{Number}] is eating...");

            Status = PhilosopherStatus.Full;
            Console.WriteLine($"🤩 Philosopher[{Number}] is full...");
        }
        finally
        {
            if (leftForkTaken)
            {
                Monitor.Exit(LeftFork);
            }

            if (rightForkTaken)
            {
                Monitor.Exit(RightFork);
            }
        }
    }

    /// <summary>
    /// Makes the philosopher think.
    /// </summary>
    public void Think()
    {
        if (Status is PhilosopherStatus.Hungry)
        {
            return;
        }

        Console.WriteLine($"🤔 Philosopher[{Number}] is thinking...");

        Status = PhilosopherStatus.Hungry;
        Console.WriteLine($"🤤 Philosopher[{Number}] is hungry...");
    }

    /// <summary>
    /// Makes the philosopher live asynchronously.
    /// </summary>
    /// <returns>A task representing the asynchronous operation.</returns>
    public async Task LiveAsync()
    {
        while (true)
        {
            await Task.Run(() =>
            {
                Eat();
                Think();
            });
        }
    }
}
