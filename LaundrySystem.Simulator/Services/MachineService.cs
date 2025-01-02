using LaundrySystem.Domain.Dtos.Cycle;
using LaundrySystem.Domain.Dtos.Machine;
using LaundrySystem.Domain.Models;
using LaundrySystem.Domain.ValueObjects;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.PortableExecutable;
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
        public async Task<bool> StartMachine(MachineDTO machine,CycleDTO cycle)
        {  
            var url = _apiURL+$"/start/{cycle.Id}";
            var content = new StringContent("1", Encoding.UTF8, "application/json");
            
            using HttpResponseMessage response = await _httpClient.PutAsync(url, content);
            response.EnsureSuccessStatusCode();
            string responseBody = await response.Content.ReadAsStringAsync();
            machine.State = MachineState.Running;
            await StartTimer(cycle.Duration , machine);

            return true;
        }

        
        public async Task<bool> StopMachine(MachineDTO machine)
        {
            var url = _apiURL + $"/stop/{machine.Id}";
            var content = new StringContent("0", Encoding.UTF8, "application/json");
            using HttpResponseMessage response = await _httpClient.PutAsync(url, content);
            response.EnsureSuccessStatusCode();
            machine.State = MachineState.Stopped;
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine($"machine id {machine.Id} has been stopped");
            Console.ForegroundColor = ConsoleColor.White;

            return true;
        }


        private async Task StartTimer(int duration , MachineDTO machine)
        {
            Timer timer = new Timer(duration * 60 * 1000);
            timer.Enabled = true;
            timer.Elapsed += (sender ,e) => OnTimedEvent(sender,e,machine,timer);
            timer.Start();   
        }

        
        private async Task OnTimedEvent(object source, ElapsedEventArgs e, MachineDTO machine, Timer timer)
        {
            await StopMachine(machine);
            timer.Stop();
            timer.Dispose(); 
        }
    }
}
