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
using SoftLineTask = SoftLineTestTask.Models.Task;

namespace SoftLineTestTask.Controllers
{
    public class TaskController : Controller
    {
        private readonly IHttpClientFactory _httpClientFactory;

        public TaskController(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }

        public async Task<IActionResult> Index(TaskDataModel model = null)
        {
            model ??= new TaskDataModel();
            
            var httpClient = _httpClientFactory.CreateClient("ApiClient");
            var request = await httpClient.GetAsync("Tasks");
            model.Tasks = JsonConvert.DeserializeObject<List<SoftLineTask>>(await request.Content.ReadAsStringAsync());

            return View(model);
        }

        public async Task<IActionResult> Delete(int id)
        {
            var httpClient = _httpClientFactory.CreateClient("ApiClient");
            var request = await httpClient.DeleteAsync($"Tasks/{id}");

            return RedirectToAction("Index", new TaskDataModel { IsSuccessDelete = request.IsSuccessStatusCode});
        }

        [HttpGet]
        public async Task<IActionResult> Add()
        {
            var taskModel = new TaskViewModel();
            
            var httpClient = _httpClientFactory.CreateClient("ApiClient");
            var request = await httpClient.GetAsync("Statuses");
            taskModel.Statuses = JsonConvert.DeserializeObject<List<Status>>(await request.Content.ReadAsStringAsync());
            
            return View(taskModel);
        }

        [HttpPost]
        public async Task<IActionResult> Add(SoftLineTask task)
        {
            var content = new StringContent(JsonConvert.SerializeObject(task), Encoding.UTF8, "application/json");
            var httpClient = _httpClientFactory.CreateClient("ApiClient");
            var request = await httpClient.PostAsync("Tasks", content);

            return RedirectToAction("Index", new TaskDataModel { IsSuccessAdded = request.IsSuccessStatusCode });
        }

        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            var taskModel = new TaskViewModel();
            
            var httpClient = _httpClientFactory.CreateClient("ApiClient");
            var request = await httpClient.GetAsync("Statuses");
            taskModel.Statuses = JsonConvert.DeserializeObject<List<Status>>(await request.Content.ReadAsStringAsync());
            request = await httpClient.GetAsync($"Tasks/{id}");
            taskModel.Task = JsonConvert.DeserializeObject<SoftLineTask>(await request.Content.ReadAsStringAsync());
            
            return View(taskModel);
        }
        
        [HttpPost]
        public async Task<IActionResult> Edit(SoftLineTask task)
        {
            var content = new StringContent(JsonConvert.SerializeObject(task), Encoding.UTF8, "application/json");
            var httpClient = _httpClientFactory.CreateClient("ApiClient");
            var request = await httpClient.PutAsync("Tasks", content);

            return RedirectToAction("Index", new TaskDataModel { IsSuccessEdit = request.IsSuccessStatusCode });
        }

        public IActionResult HandleTask(int id, string action)
        {
            if (action == "edit")
                return RedirectToAction("Edit", new { id });
            if (action == "delete")
                return RedirectToAction("Delete", new { id });

            return RedirectToAction("Index");
        }
    }
}