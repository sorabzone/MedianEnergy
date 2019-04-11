using MedianEnergy.Engine.Interface;
using MedianEnergy.Engine.Service;
using System.Threading.Tasks;

namespace MedianEnergy.Engine
{
    public class EnergyWorker
    {
        private readonly LPService _lpService;
        private readonly TOUService _touService;

        public EnergyWorker(LPService lpService, TOUService touService, IOutputService outputService)
        {
            _lpService = lpService;
            _touService = touService;
        }

        /// <summary>
        /// This function process both type of files. First it will process LP files and then TOU files.
        /// We can process them in parallel as well, but that depends on requirement and business operations
        /// </summary>
        /// <param name="lpFilePath"></param>
        /// <param name="touFilePath"></param>
        /// <returns></returns>
        public async Task StartProcessing(string lpFilePath, string touFilePath)
        {
            await _lpService.ProcessFiles(lpFilePath);
            await _touService.ProcessFiles(touFilePath);
        }
    }
}
