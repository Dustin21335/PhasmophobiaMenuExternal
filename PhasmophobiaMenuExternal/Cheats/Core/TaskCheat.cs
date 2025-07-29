namespace PhasmophobiaMenuExternal.Cheats.Core
{
    public abstract class TaskCheat : Cheat
    {
        private CancellationTokenSource cancellationTokenSource;
        private Task task;
        public int Delay = 500;

        public override void OnEnableCore()
        {
            cancellationTokenSource = new CancellationTokenSource();
            task = Task.Run(async () =>
            {
                while (!cancellationTokenSource.Token.IsCancellationRequested)
                {
                    await Update();
                    await Task.Delay(Delay);
                }
            });
        }

        public override void OnDisableCore()
        {
            if (cancellationTokenSource != null)
            {
                cancellationTokenSource.Cancel();
                cancellationTokenSource.Dispose();
                cancellationTokenSource = null;
            }
        }

        public abstract Task Update();
    }
}
