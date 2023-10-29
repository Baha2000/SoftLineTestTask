using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using SoftLineTestTask.Data;
using SoftLineTestTask.Models;
using SoftLineTestTask.Models.Entity;
using SoftLineTask = SoftLineTestTask.Models.Entity.Task;

namespace SoftLineTestTask.Controllers
{
    public class TaskController : Controller
    {
        private readonly IHttpClientFactory _httpClientFactory;

        public TaskController(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }

        public async Task<IActionResult> Index(TasksViewModel model = null)
        {
            model ??= new TasksViewModel();
            
            var httpClient = _httpClientFactory.CreateClient("ApiClient");
            var request = await httpClient.GetAsync("Tasks");
            model.Tasks = JsonConvert.DeserializeObject<List<SoftLineTask>>(await request.Content.ReadAsStringAsync());

            return View(model);
        }

        public async Task<IActionResult> Delete(int? id)
        {
            var httpClient = _httpClientFactory.CreateClient("ApiClient");
            var request = await httpClient.DeleteAsync($"Tasks/{id}");

            return RedirectToAction("Index", new TasksViewModel { IsSuccessDelete = request.IsSuccessStatusCode});
        }

        [HttpGet]
        public async Task<IActionResult> Add()
        {
            var taskModel = new TaskViewModel();
            
            var httpClient = _httpClientFactory.CreateClient("ApiClient");
            taskModel.Statuses = await GetStatuses(httpClient);

            return View(taskModel);
        }

        [HttpPost]
        public async Task<IActionResult> Add(SoftLineTask task)
        {
            if (!ModelState.IsValid)
                return View(new TaskViewModel { Task = task, Statuses = await GetStatuses() });
            
            var content = new StringContent(JsonConvert.SerializeObject(task), Encoding.UTF8, "application/json");
            var httpClient = _httpClientFactory.CreateClient("ApiClient");
            var request = await httpClient.PostAsync("Tasks", content);

            return RedirectToAction("Index", new TasksViewModel { IsSuccessAdded = request.IsSuccessStatusCode });
        }

        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            var taskModel = new TaskViewModel();
            
            var httpClient = _httpClientFactory.CreateClient("ApiClient");
            taskModel.Statuses = await GetStatuses(httpClient);
            var request = await httpClient.GetAsync($"Tasks/{id}");
            taskModel.Task = JsonConvert.DeserializeObject<SoftLineTask>(await request.Content.ReadAsStringAsync());
            
            return View(taskModel);
        }
        
        [HttpPost]
        public async Task<IActionResult> Edit(SoftLineTask task)
        {
            if (!ModelState.IsValid)
                return View(new TaskViewModel { Task = task, Statuses = await GetStatuses() });

            var content = new StringContent(JsonConvert.SerializeObject(task), Encoding.UTF8, "application/json");
            var httpClient = _httpClientFactory.CreateClient("ApiClient");
            var request = await httpClient.PutAsync("Tasks", content);

            return RedirectToAction("Index", new TasksViewModel { IsSuccessEdit = request.IsSuccessStatusCode });
        }

        public IActionResult HandleTask(int id, string action)
        {
            return action switch
            {
                "edit" => RedirectToAction("Edit", new { id }),
                "delete" => RedirectToAction("Delete", new { id }),
                _ => RedirectToAction("Index")
            };
        }


        public async Task<IEnumerable<Status>> GetStatuses(HttpClient httpClient = null)
        {
            httpClient ??= _httpClientFactory.CreateClient("ApiClient");
            var request = await httpClient.GetAsync("Statuses");
            return JsonConvert.DeserializeObject<List<Status>>(await request.Content.ReadAsStringAsync());
        }
    }
}