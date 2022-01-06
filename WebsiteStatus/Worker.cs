namespace WebsiteStatus
{
    /// <summary>
    /// The worker is everything it needs to do
    /// </summary>
    public class Worker : BackgroundService
    {
        private readonly ILogger<Worker> _logger;
        private readonly string _url = "https://coryf.dev";
        private readonly int _delay = 5 * 1000; // 60 * 1000 = one minute
        private HttpClient _httpClient;

        /// <summary>
        /// Logging info
        /// </summary>
        /// <param name="logger"></param>
        public Worker(ILogger<Worker> logger)
        {
            _logger = logger;
        }

        // Start
        public override Task StartAsync(CancellationToken cancellationToken)
        {
            // create client on start
            _httpClient = new HttpClient();
            _logger.LogInformation("Service Started.");
            return base.StartAsync(cancellationToken); // do whatever the base does
        }

        public override Task StopAsync(CancellationToken cancellationToken)
        {
            // What happens on stop
            _httpClient.Dispose();
            _logger.LogInformation("Service Stopped.");
            return base.StopAsync(cancellationToken);
        }

        /// <summary>
        /// This is what runs
        /// </summary>
        /// <param name="stoppingToken"></param>
        /// <returns></returns>
        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            // pass in a cancel token to stop the process
            while (!stoppingToken.IsCancellationRequested)
            {
                var result = await _httpClient.GetAsync(_url);

                if (result.IsSuccessStatusCode)
                {
                    // 
                    _logger.LogInformation($"The website {_url} is up, with a status of {result.StatusCode}");
                }
                else
                {
                    // some code to report this. send email, text or some other action

                    // site down
                    _logger.LogError($"the website {_url} is down. The status is {result.StatusCode}");
                }

                await Task.Delay(_delay, stoppingToken); // wait
            }
        }
    }
}