using LaundrySystem.Domain.Models;
using LaundrySystem.Domain.Dtos.Laundry;
using LaundrySystem.Domain.Dtos.Machine;
using LaundrySystem.Domain.Dtos.Owner;
using LaundrySystem.Domain.Dtos.Cycle;
using LaundrySystem.Simulator.Services;
using LaundrySystem.Domain.ValueObjects;


string BaseUrl = "https://localhost:7102/api";
ConfigurationService _configurationService = new ConfigurationService(BaseUrl+"/configuration");
MachineService _machineService = new MachineService(BaseUrl + "/Machine"); 


Console.WriteLine("Enter Owner Id : ");

var ownerId = Console.ReadLine();
OwnerDTO owner = await LoadOwnerConfig(ownerId);
Console.WriteLine($"Welcome Mr {owner.Name}");

while (true)
{
    DisplayTextWithColor("Please select an option:",ConsoleColor.Magenta);
    Console.WriteLine("1. View All Laveries");
    Console.WriteLine("2. Manage Laveries");
    Console.WriteLine("3. Exit");

    var choice = Console.ReadLine();
    switch (choice)
    {
        case "1":
            DisplayAllLaveries(owner);
            break;
        case "2":
            LaundryDTO selectedLaundry = SelectLaverie(owner);
            if (selectedLaundry != null)
            {
                DisplayAllMachines(selectedLaundry);
                var selectedMachine = SelectMachine(selectedLaundry);
                if (selectedMachine != null)
                {
                    DisplayAllCycles(selectedMachine);
                    var selectedCycle = SelectCycle(selectedMachine);
                    if (selectedCycle != null) { 
                         
                        if(await _machineService.StartMachine(selectedCycle))
                        {
                            DisplayTextWithColor("machine started successfully", ConsoleColor.Green);
                        }
                    }
                }
            }
            break;
        case "3":
            DisplayTextWithColor("Exited", ConsoleColor.Red);
            return;
    }

}

MachineDTO SelectMachine(LaundryDTO laverie)
{
    DisplayTextWithColor("please enter the machine id:", ConsoleColor.Magenta);
    var machineId = Console.ReadLine();
    var machine = laverie.Machines.FirstOrDefault(x => x.Id.ToString().Equals(machineId));
    if (machine == null)
    {
        DisplayTextWithColor("Invalid Machine", ConsoleColor.Red);
        
    }
    return machine;
}

LaundryDTO SelectLaverie(OwnerDTO owner)
{
    LaundryDTO? laverie = null;

    DisplayTextWithColor("please enter the laundry id:",ConsoleColor.Magenta);
    var LaverieId = int.Parse(Console.ReadLine());
    laverie = owner.Laundries.FirstOrDefault(x => x.Id == LaverieId);
    if (laverie == null)
    {
        DisplayTextWithColor("Invalid Laundry", ConsoleColor.Red);

    }

    return laverie;
}

CycleDTO SelectCycle(MachineDTO machine)
{
    CycleDTO? cycle = null;

    DisplayTextWithColor("please enter the cycle id:", ConsoleColor.Magenta);
    var cycleId = int.Parse(Console.ReadLine());
    cycle = machine.Cycles.FirstOrDefault(x => x.Id == cycleId);
    if (cycle == null)
    {
        DisplayTextWithColor("cycle Invalid:", ConsoleColor.Red);
    }
    return cycle;
}
void DisplayAllCycles(MachineDTO machine)
{
    DisplayTextWithColor($"Machine id {machine.Id} cycles:",ConsoleColor.Magenta);
    foreach (var cycleDto in machine.Cycles)
    {
        Console.WriteLine($"Id : {cycleDto.Id} - Price: {cycleDto.Cout} - Duration :{cycleDto.Duration} - Total Actions : {cycleDto.Actions.Count} ");
    }
}

void DisplayAllLaveries(OwnerDTO owner)
{
    DisplayTextWithColor("This is your Laundries:",ConsoleColor.Magenta);
    foreach (LaundryDTO laverieDto in owner.Laundries)
    {
        Console.WriteLine($"Id : {laverieDto.Id} - Name : {laverieDto.Name} - Number Of Machines :{laverieDto.Machines.Count} ");
    }
}


void DisplayAllMachines(LaundryDTO laverie)
{
    DisplayTextWithColor($"Machines of Laverie {laverie.Name} : ",ConsoleColor.Magenta);
    foreach (MachineDTO machine in laverie.Machines)
    {
        Console.Write($"Id : {machine.Id} - Model: {machine.Model} - number of cycles : {machine.Cycles.Count} - status : ");
        if (machine.State == MachineState.Running)
        {
            DisplayTextWithColor("Running", ConsoleColor.Green);
        }
        if (machine.State == MachineState.Stopped)
        {
            DisplayTextWithColor("Stopping", ConsoleColor.Red);

        }
        Console.WriteLine("");

    }

}

async Task<OwnerDTO> LoadOwnerConfig(string id)
{
    try
    {
        var owner = await _configurationService.GetOwnerConfig(id);

        Console.Write("Loading Configuration");
        for (int i = 0; i < 3; i++)
        {
            Console.Write(".");
            Thread.Sleep(1000);
        }
        Console.WriteLine();

        DisplayTextWithColor("Loaded Configuration Successfully", ConsoleColor.Green);

        return owner;

    }
    catch (HttpRequestException e)
    {

        DisplayTextWithColor("Failed to load Configuration", ConsoleColor.Red);
        return null;

    }

}

void DisplayTextWithColor(string text, ConsoleColor color)
{
    Console.ForegroundColor = color;
    Console.WriteLine($"{text}");
    Console.ResetColor();
}