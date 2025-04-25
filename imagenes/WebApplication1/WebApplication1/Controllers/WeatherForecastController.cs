using Microsoft.AspNetCore.Mvc;
using YourNamespace.Models;
using System;


namespace YourNamespace.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ChoresController : ControllerBase
    {
        private static readonly Dictionary<int, List<string>> _choresByDay = new()
        {
            { 1, new List<string> { "Limpiar cocina" } },     // Lunes
            { 2, new List<string> { "Lavar ropa" } },         // Martes
            { 3, new List<string> { "Sacar la basura" } },    // Miércoles
            { 4, new List<string>() },                        // Jueves
            { 5, new List<string>() },                        // Viernes
            { 6, new List<string>() },                        // Sábado
            { 7, new List<string>() }                         // Domingo
        };

        [HttpGet("{dayId:int}")]
        public IActionResult GetChoresByDay(int dayId)
        {
            if (dayId < 1 || dayId > 7)
                return BadRequest("El día debe estar entre 1 y 7");

            var chores = _choresByDay.ContainsKey(dayId) ? _choresByDay[dayId] : new List<string>();
            return Ok(chores);
        }

        [HttpPost]
        public IActionResult AddChore([FromBody] Chore newChore)
        {
            if (newChore.DayId < 1 || newChore.DayId > 7)
                return BadRequest("El día debe estar entre 1 y 7");

            if (!_choresByDay.ContainsKey(newChore.DayId))
                _choresByDay[newChore.DayId] = new List<string>();

            _choresByDay[newChore.DayId].Add(newChore.Activity);
            return Ok($"Actividad agregada al día {newChore.DayId}");
        }
    }
}

