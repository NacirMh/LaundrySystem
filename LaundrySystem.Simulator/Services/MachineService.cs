using LaundrySystem.Domain.Dtos.Cycle;
using LaundrySystem.Domain.Models;
using LaundrySystem.Domain.ValueObjects;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Timers;
using Timer = System.Timers.Timer;


namespace LaundrySystem.Simulator.Services
{
    public class MachineService
    {
        private readonly HttpClient _httpClient;
        private readonly string _apiURL;

        public MachineService(string apiURL)
        {
            _httpClient = new HttpClient(); 

            _apiURL = apiURL;
        }
        public async Task<bool> StartMachine(CycleDTO cycle)
        {  
            var url = _apiURL+$"/start/{cycle.Id}";
            var content = new StringContent("1", Encoding.UTF8, "application/json");
            using HttpResponseMessage response = await _httpClient.PutAsync(url, content);
            response.EnsureSuccessStatusCode();
            string responseBody = await response.Content.ReadAsStringAsync();
            await StartTimer(cycle.Duration , cycle.MachineId);
            return true;
        }

        


        private async Task StartTimer(int duration , int machineId)
        {
            Timer timer = new Timer(duration * 60 * 1000);
            timer.Enabled = true;
            timer.Elapsed += (sender ,e) => OnTimedEvent(sender,e,machineId,timer);
            timer.Start();   
        }

        
        private async Task OnTimedEvent(object source, ElapsedEventArgs e, int machineId, Timer timer)
        {
            var url = _apiURL + $"/stop/{machineId}";
            var content = new StringContent("0", Encoding.UTF8, "application/json");
            using HttpResponseMessage response = await _httpClient.PutAsync(url, content);
            response.EnsureSuccessStatusCode();
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("machine stopped");
            Console.ForegroundColor = ConsoleColor.White;
            timer.Stop();
            timer.Dispose(); 
        }


    }
}
