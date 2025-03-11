using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;
using System.Reflection;

namespace Week1_Assignment.Controllers;

public class Device  
{
    public int Id { get; set; }

    [Required]
    public string Type { get; set; }

    [Required]
    public string IpAddress { get; set; }

    public DateTime LastUpdateTime { get; set; }

    [Required]
    public string Location { get; set; }
}

public class DeviceViewModel //this view model is needed later for updating/creating the device without ability to change it's id 
{
    public string Type { get; set; }

    public string Location { get; set; }

    public DateTime LastUpdateTime { get; set; }

    public string IpAddress { get; set; }
}

[Route("api/[controller]")]
[ApiController]
public class DeviceController : ControllerBase
{
    public static List<Device> _devices = new List<Device> //creating a static list to get some examples 
    {
        new Device { Id = 1,  Type = "Computer", IpAddress = "123.123.12.123", LastUpdateTime = new DateTime(2023, 04, 12),
                            Location = "Manager's Room" },
        new Device { Id = 2, Type = "Printer", IpAddress = "192.168.1.10", LastUpdateTime = new DateTime(2024, 01, 05),
                            Location = "Manager's Room" },
        new Device { Id = 3, Type = "Router", IpAddress = "10.0.0.1", LastUpdateTime = new DateTime(2024, 02, 20),
                            Location = "Server Room" },
        new Device { Id = 4, Type = "Tablet", IpAddress = "172.16.5.8", LastUpdateTime = new DateTime(2024, 03, 10),
                            Location = "Reception" },
        new Device { Id = 5, Type = "Smartphone", IpAddress = "192.168.0.15", LastUpdateTime = new DateTime(2024, 03, 08),
                            Location = "CEO's Office" },
        new Device { Id = 6, Type = "Laptop", IpAddress = "192.168.1.20", LastUpdateTime = new DateTime(2024, 03, 15),
                            Location = "IT Department" },
        new Device { Id = 7, Type = "Smart TV", IpAddress = "172.16.10.5", LastUpdateTime = new DateTime(2024, 02, 28),
                            Location = "Conference Room" },
        new Device { Id = 8, Type = "Server", IpAddress = "10.10.10.10", LastUpdateTime = new DateTime(2024, 03, 01),
                            Location = "Data Center" },
        new Device { Id = 9, Type = "Security Camera", IpAddress = "192.168.2.50", LastUpdateTime = new DateTime(2024, 03, 09),
                            Location = "Warehouse" },
        new Device { Id = 10, Type = "Smart Speaker", IpAddress = "192.168.3.30", LastUpdateTime = new DateTime(2024, 03, 12),
                            Location = "Lounge Area" }
    };

    [HttpGet]
    public ActionResult<IEnumerable<Device>> GetAll() //listing all the devices 
    {
        return Ok(_devices);
    }

    [HttpGet("{id}")]
    public IActionResult GetById([FromRoute] int id) //listing device special to that id by getting id from the route
    {
        var device = _devices.FirstOrDefault(d => d.Id == id);

        if (device == null)
            return NotFound("The id does not exist");

        return Ok(device);
    }

    [HttpGet("list-by-location")]
    public IActionResult ListBy([FromQuery] string location) //listing devices at that location given from the query
    {
        var filteredDevices = _devices.Where(d => string.Equals(d.Location, location, StringComparison.OrdinalIgnoreCase)).ToList();

        if (filteredDevices == null)
            return NotFound("There are no device in this location");

        return Ok(filteredDevices);
    }

    [HttpPost]
    public IActionResult Create([FromBody] DeviceViewModel newDevice) //deviceViewModel is used so that id cannot be given by user 
    {                                                                   
        if (!ModelState.IsValid)
            return BadRequest(newDevice); //bad request is returning as an error

        var device = _devices.FirstOrDefault(d => d.IpAddress == newDevice.IpAddress);

        if (device != null)
            return BadRequest("This device already exists");

        var newOne = new Device
        {
            Id = _devices.Max(p => p.Id) + 1,   //id is given uniquely by the program not by user 
            Type = newDevice.Type,
            LastUpdateTime = newDevice.LastUpdateTime,
            IpAddress = newDevice.IpAddress,
            Location = newDevice.Location
        };

        _devices.Add(newOne);

        return CreatedAtAction(nameof(GetById), new { id = newOne.Id }, newOne);  //returning the created device info
    }

    [HttpPut("{id}")]
    public IActionResult Update(int id, [FromBody] DeviceViewModel updatedVersion)
    {
        var device = _devices.FirstOrDefault(d => d.Id == id);
        if (device == null)
            return NotFound("Product not found"); //returning 404 not found 

        device.Id = id;  //id cannot be changed by user with using deviceViewModel
        device.Type = updatedVersion.Type;
        device.IpAddress = updatedVersion.IpAddress;
        device.LastUpdateTime = updatedVersion.LastUpdateTime;
        device.Location = updatedVersion.Location;
        
        return Ok(device);
    }

    [HttpPatch("{id}")]
    public IActionResult PartialUpdate(int id, [FromBody] Dictionary<string, object> updates) 
    {
        var device = _devices.FirstOrDefault(d => d.Id == id);
        if (device == null)
            return NotFound("Product not found");

        if (updates.ContainsKey("type")) //if the user write type in the first space it will update type of the device where the second 
            device.Type = updates["type"].ToString();  //space is filled
        if (updates.ContainsKey("ipAddress"))
            device.IpAddress = updates["ipAddress"].ToString();
        if (updates.ContainsKey("lastUpdatedTime"))
            device.LastUpdateTime = Convert.ToDateTime(updates["lastUpdatedTime"]);
        if (updates.ContainsKey("location"))
            device.Location = updates["location"].ToString();

        return Ok(device);
    }

    [HttpDelete("{id}")]
    public IActionResult Delete(int id)
    {
        var device = _devices.FirstOrDefault(d => d.Id == id);
        if (device == null)
            return NotFound("Product not found");

        _devices.Remove(device);

        return NoContent(); 
    }
}

