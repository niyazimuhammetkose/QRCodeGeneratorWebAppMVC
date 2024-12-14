using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using QrCodeGeneratorWebAppMVC.Data;
using QrCodeGeneratorWebAppMVC.Models;
using QrCodeGeneratorWebAppMVC.Models.ViewModels.QrCodes;
using QrCodeGeneratorWebAppMVC.Services;

namespace QrCodeGeneratorWebAppMVC.Controllers
{
    public class QrCodesController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly QrCodeService _qrCodeService;
        private readonly IWebHostEnvironment _webHostEnvironment;

        public QrCodesController(ApplicationDbContext context, QrCodeService qrCodeService, IWebHostEnvironment webHostEnvironment)
        {
            _context = context;
            _qrCodeService = qrCodeService;
            _webHostEnvironment = webHostEnvironment;
        }

        //// GET: QrCodes
        //public async Task<IActionResult> Index()
        //{
        //    return View(await _context.QrCode.ToListAsync());
        //}

        // GET: QrCodes
        public async Task<IActionResult> Index(int page = 1, int pageSize = 10, string qrCodeType = "", string searchString = "")
        {
            if (_context.QrCode == null)
            {
                return Problem("Entity set 'ApplicationDbContext.QrCode' is null.");
            }

            // Use LINQ to get list of genres.
            IQueryable<string> typeQuery = from m in _context.QrCode
                                           orderby m.Type
                                           select m.Type;

            var qrCodes = from m in _context.QrCode
                          select m;

            if (!string.IsNullOrEmpty(searchString))
            {
                qrCodes = qrCodes.Where(s => s.Name!.ToUpper().Contains(searchString.ToUpper()));
            }

            if (!string.IsNullOrEmpty(qrCodeType))
            {
                qrCodes = qrCodes.Where(x => x.Type == qrCodeType);
            }

            var viewModel = new QrCodeIndexViewModel
            {
                QrCodeTypes = new SelectList(await typeQuery.Distinct().ToListAsync()),
                QrCodes = await qrCodes.ToListAsync(),

                PagedQrCodes = qrCodes
                            .Skip((page - 1) * pageSize)
                            .Take(pageSize)
                            .ToList(),
                CurrentPage = page,
                TotalItems = qrCodes.Count(),
                PageSize = pageSize
            };

            return View(viewModel);
        }

        // GET: QrCodes/Details/5
        [Authorize]
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var qrCode = await _context.QrCode
                .FirstOrDefaultAsync(m => m.Id == id);
            if (qrCode == null)
            {
                return NotFound();
            }

            return View(qrCode);
        }

        // GET: QrCodes/Create
        [Authorize]
        public IActionResult Create()
        {
            return View();
        }

        // POST: QrCodes/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize]
        public async Task<IActionResult> Create([Bind("Id,Name,Type,ImagePath,Url,VCardFirstName,VCardLastName,VCardNickName,VCardOrg,VCardOrgTitle,VCardPhone,VCardMobilePhone,VCardWorkPhone,VCardEmail,VCardBirthDay,VCardWebsite,VCardStreet,VCardHouseNumber,VCardCity,VCardStateRegion,VCardZipCode,VCardCountry,VCardNote")] QrCode qrCode)
        {
            if (ModelState.IsValid)
            {
                var filename = "";

                if (qrCode.Type == "url")
                {
                    filename = "qrcode_url_" + Guid.NewGuid() + ".png";
                    _qrCodeService.GenerateUrlQrCode(qrCode.Url, filename);
                    qrCode.ImagePath = _qrCodeService.RelativePath;
                }
                else if (qrCode.Type == "vCard")
                {
                    filename = "qrcode_vcard_" + Guid.NewGuid() + ".png";
                    VCardContactData vCardContactData = new()
                    {
                        FirstName = qrCode.VCardFirstName,
                        LastName = qrCode.VCardLastName,
                        NickName = qrCode.VCardNickName,
                        Org = qrCode.VCardOrg,
                        OrgTitle = qrCode.VCardOrgTitle,
                        Phone = qrCode.VCardPhone,
                        MobilePhone = qrCode.VCardMobilePhone,
                        WorkPhone = qrCode.VCardWorkPhone,
                        Email = qrCode.VCardEmail,
                        BirthDay = qrCode.VCardBirthDay,
                        Website = qrCode.VCardWebsite,
                        Street = qrCode.VCardStreet,
                        HouseNumber = qrCode.VCardHouseNumber,
                        City = qrCode.VCardCity,
                        StateRegion = qrCode.VCardStateRegion,
                        ZipCode = qrCode.VCardZipCode,
                        Country = qrCode.VCardCountry,
                        Note = qrCode.VCardNote
                    };
                    _qrCodeService.GenerateVCardQrCode(vCardContactData, filename);
                    qrCode.ImagePath = _qrCodeService.RelativePath;
                }

                _context.Add(qrCode);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(qrCode);
        }

        // GET: QrCodes/Edit/5
        [Authorize]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var qrCode = await _context.QrCode.FindAsync(id);
            if (qrCode == null)
            {
                return NotFound();
            }
            return View(qrCode);
        }

        // POST: QrCodes/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name,Type,ImagePath,Url,VCardFirstName,VCardLastName,VCardNickName,VCardOrg,VCardOrgTitle,VCardPhone,VCardMobilePhone,VCardWorkPhone,VCardEmail,VCardBirthDay,VCardWebsite,VCardStreet,VCardHouseNumber,VCardCity,VCardStateRegion,VCardZipCode,VCardCountry,VCardNote")] QrCode qrCode)
        {
            if (id != qrCode.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(qrCode);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!QrCodeExists(qrCode.Id))
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
            return View(qrCode);
        }

        // GET: QrCodes/Delete/5
        [Authorize]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var qrCode = await _context.QrCode
                .FirstOrDefaultAsync(m => m.Id == id);
            if (qrCode == null)
            {
                return NotFound();
            }

            return View(qrCode);
        }

        // POST: QrCodes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var qrCode = await _context.QrCode.FindAsync(id);
            if (qrCode != null)
            {
                _context.QrCode.Remove(qrCode);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool QrCodeExists(int id)
        {
            return _context.QrCode.Any(e => e.Id == id);
        }

        // GET: QrCodes/Download/5
        [Authorize]
        public async Task<IActionResult> Download(int id)
        {
            var qrCode = await _context.QrCode
                .FirstOrDefaultAsync(m => m.Id == id);
            if (qrCode == null)
            {
                return NotFound();
            }

            string fullPath = Path.Combine(_webHostEnvironment.WebRootPath, qrCode.ImagePath);
            byte[] fileBytes = System.IO.File.ReadAllBytes(fullPath);
            return File(fileBytes, "application/octet-stream", Path.GetFileName(fullPath));
        }
    }
}
