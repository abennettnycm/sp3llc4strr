using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using static sp3llc4strr.Models.SpellModels;

namespace sp3llc4strr.Controllers
{
	public class HomeController : Controller
	{
		public ActionResult Index()
		{
			return View();
		}

		[HttpPost]
		public JsonResult GetRandomSpell()
		{
			SpellBook spellbook = new SpellBook();
			Random rand = new Random();
			int rn = rand.Next(0, spellbook.spells.Count);

			return Json(spellbook.spells[rn]);
		}

		[HttpPost]
		public JsonResult RollD20()
		{
			Random rand = new Random();
			int d20 = rand.Next(1, 21); // inclusive, exclusive

			return Json(new { roll = d20 });
		}

		[HttpPost]
		public JsonResult RollDamage(string dice)
		{
			Random rand = new Random();
			int dmg = rand.Next(1, int.Parse(dice) + 1); // add 1 since upper bound is exclusive

			return Json(new { damage = dmg });
		}
	}
}