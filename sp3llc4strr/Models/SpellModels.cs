using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace sp3llc4strr.Models
{
	public class SpellModels
	{
		public class Spell
		{
			public int level { get; set; }
			public string name { get; set; }
			public string description { get; set; }
			public bool rollToHit { get; set; }
			public SavingThrow save { get; set; }
			public int howManyDice { get; set; }
			public int diceType { get; set; }
			public int diceTypeAlternate { get; set; }
		}

		public class SavingThrow
		{
			public string type { get; set; }
			public int number { get; set; }
			public bool halfOnFail { get; set; }
		}

		public class SpellBook
		{
			private const int STANDARD_SAVE_NUM = 13;
			public List<Spell> spells = new List<Spell>();

			public SpellBook()
			{
				spells.Add(new Spell
				{
					level = 0,
					name = "Acid Splash",
					description = "You hurl a bubble of acid. A target must succeed on a Dexterity saving throw or take 1d6 acid damage.",
					rollToHit = false,
					save = new SavingThrow()
					{
						type = "DEX",
						number = STANDARD_SAVE_NUM,
						halfOnFail = false
					},
					howManyDice = 1,
					diceType = 6,
					diceTypeAlternate = -1
				});

				spells.Add(new Spell
				{
					level = 0,
					name = "Fire Bolt",
					description = "You hurl a mote of fire. Make a ranged spell attack against the target. On a hit, the target takes 1d10 fire damage.",
					rollToHit = true,
					save = null,
					howManyDice = 1,
					diceType = 10,
					diceTypeAlternate = -1
				});

				spells.Add(new Spell
				{
					level = 0,
					name = "Ray of Frost",
					description = "A frigid beam of blue-white light streaks toward a creature. Make a ranged spell attack against the target. On a hit, it takes 1d8 cold damage.",
					rollToHit = true,
					save = null,
					howManyDice = 1,
					diceType = 8,
					diceTypeAlternate = -1
				});

				spells.Add(new Spell
				{
					level = 0,
					name = "Infestation",
					description = "You cause a cloud of mites, fleas, and other parasites to appear momentarily on one creature you can see within range. The target must succeed on a Constitution saving throw, or it takes 1d6 poison damage.",
					rollToHit = false,
					save = new SavingThrow()
					{
						type = "CON",
						number = STANDARD_SAVE_NUM,
						halfOnFail = false
					},
					howManyDice = 1,
					diceType = 6,
					diceTypeAlternate = -1
				});

				spells.Add(new Spell
				{
					level = 0,
					name = "Toll the Dead",
					description = "You point at one creature you can see within range, and the sound of a dolorous bell fills the air around it for a moment. The target must succeed on a Wisdom saving throw or take 1d8 necrotic damage. If the target is missing any of its hit points, it instead takes 1d12 necrotic damage.",
					rollToHit = false,
					save = new SavingThrow()
					{
						type = "CON",
						number = STANDARD_SAVE_NUM,
						halfOnFail = false
					},
					howManyDice = 1,
					diceType = 8,
					diceTypeAlternate = 12
				});
			}
		}
	}
}