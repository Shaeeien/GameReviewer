using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using GameReviewer.Models;

namespace GameReviewer.Controllers
{
    public class ProducersController : Controller
    {
        private readonly IRepository<Producer> _repo;

        public ProducersController(IRepository<Producer> repo)
        {
            _repo = repo;
        }

        // GET: Producers
        public IActionResult Index()
        {
              return View(_repo.GetAll());
        }

        // GET: Producers/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _repo.GetAll() == null)
            {
                return NotFound();
            }

            var producer = _repo.GetAll().FirstOrDefault(m => m.Id == id);
            if (producer == null)
            {
                return NotFound();
            }

            return View(producer);
        }

        // GET: Producers/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Producers/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create([Bind("Id,Name,Desciption")] Producer producer)
        {
            if (_repo.Add(producer))
            {
                return View();
            }
            return RedirectToAction(nameof(Index));
            //return View(producer);
        }

        // GET: Producers/Edit/5
        public IActionResult Edit(int? id)
        {
            if (id == null || _repo.GetAll() == null)
            {
                return NotFound();
            }

            var producer = _repo.GetAll().Where(x => x.Id == id);
            if (producer == null)
            {
                return NotFound();
            }
            return View(producer);
        }

        // POST: Producers/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(int id, [Bind("Id,Name,Desciption")] Producer producer)
        {
            if (id != producer.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _repo.Update(id, producer);
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ProducerExists(producer.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(producer);
        }

        // GET: Producers/Delete/5
        public IActionResult Delete(int? id)
        {
            if (id == null || _repo.GetAll() == null)
            {
                return NotFound();
            }

            var producer = _repo.GetAll().FirstOrDefault(m => m.Id == id);
            if (producer == null)
            {
                return NotFound();
            }

            return View(producer);
        }

        // POST: Producers/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(int id)
        {
            if (_repo.GetAll() == null)
            {
                return Problem("Entity set 'ReviewContext.Producers'  is null.");
            }
            Producer producer = _repo.GetAll().Where(x => x.Id == id).FirstOrDefault();
            if (producer != null)
            {
                _repo.Remove(producer);
            }
            
            return RedirectToAction(nameof(Index));
        }

        private bool ProducerExists(int id)
        {
            Producer producer = _repo.GetAll().FirstOrDefault(x => x.Id == id);
            if(producer != null)
                return _repo.Exists(producer);
            return false;
        }
    }
}
