namespace Darp.DAQmx.Task;

public enum TaskAction
{
    /// <summary>Transitions the task to the running state, which begins device input or output.</summary>
    Start,
    /// <summary>Transitions the task from the running state to the committed state, which ends device input or output.
    /// </summary>
    Stop,
    /// <summary>Verifies that all task parameters are valid for the hardware.</summary>
    Verify,
    /// <summary>Programs the hardware with all parameters of the task.</summary>
    Commit,
    /// <summary>Marks the hardware resources that are needed for the task as in use. No other tasks can reserve these same resources.</summary>
    Reserve,
    /// <summary>Releases all previously reserved resources.</summary>
    Unreserve,
    /// <summary>
    /// Aborts execution of the task. Aborting a task immediately terminates the currently active operation, such as a read or a write. Aborting a task puts the task into an unstable but recoverable state. To recover the task, use <see cref="M:NationalInstruments.DAQmx.Task.Start" /> to restart the task or use <see cref="M:NationalInstruments.DAQmx.Task.Stop" /> to reset the task without starting it.
    /// </summary>
    Abort,
}