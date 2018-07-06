var caster = 1;
var target = 2;
var ac = 13;
var dc = 13;
var diceType = 0;

$(document).ready(function () {
	enableButton(caster, "cast");

	$(".cast").click(function (event) {
		castRandomSpell();
	});

	$(".hit").click(function (event) {
		rollToHit();
	});

	$(".save").click(function (event) {
		savingThrow();
	});

	$(".damage").click(function (event) {
		rollDamage();
	});

	$(".death").click(function (event) {
		rollDeathSave();
	});
});

function disableButton(num, btn) {
	var selector = "#caster-" + num + " ." + btn;
	if (btn == "ALL") {
		selector = "#caster-" + num + " a";
	}

	$(selector).addClass("disabled");
	$(selector).removeClass("btn-primary");
	$(selector).addClass("btn-default");
}

function enableButton(num, btn) {
	var selector = "#caster-" + num + " ." + btn;
	if (selector == "ALL") {
		selector = "#caster-" + num + " a";
	}

	$(selector).removeClass("disabled");
	$(selector).addClass("btn-primary");
	$(selector).removeClass("btn-default");
}

function castRandomSpell() {
	$.ajax({
		url: "/Home/GetRandomSpell",
		type: "POST",
		cache: false
	}).done(function (json) {
		disableButton(caster, "cast");

		$(".spell-name").html(json.name);
		$(".spell-level").html(json.level == 0 ? "Cantrip" : "Level " + json.level);
		$(".spell-desc").html(json.description);

		if (json.rollToHit == true) {
			$(".spell-desc").append("<hr />Roll To Hit...");
			enableButton(caster, "hit");
		}
		else if (json.save != null) {
			enableButton(target, "save");
		}

		diceType = json.diceType;
	});
}

function rollToHit() {
	$.ajax({
		url: "/Home/RollD20",
		type: "POST",
		cache: false
	}).done(function (json) {
		disableButton(caster, "hit");

		$(".spell-desc").append("<br />" + json.roll + "<hr />");
		if (json.roll >= ac) {
			$(".spell-desc").append("<p>You hit!</p>Roll Damage...");
			enableButton(caster, "damage");
		}
		else {
			$(".spell-desc").append("<p>You missed.</p>");
			switchTurn();
		}
	});
}

function rollDamage() {
	$.ajax({
		url: "/Home/RollDamage",
		type: "POST",
		data: {
			dice: diceType
		},
		cache: false
	}).done(function (json) {
		disableButton(target, "save");

		$(".spell-desc").append("<hr />Spell dealt <b>" + json.damage + "</b> damage!");

		var hp = $("#caster-" + target + " .curr-hp").text();
		hp -= json.damage;
		$("#caster-" + target + " .curr-hp").text(hp);

		if (hp > 0) {
			switchTurn();
		}
		else {
			// TODO: death saves for target
		}
		
	});
}

function savingThrow() {
	$.ajax({
		url: "/Home/RollD20",
		type: "POST",
		cache: false
	}).done(function (json) {
		disableButton(target, "save");

		$(".spell-desc").append("<br />" + json.roll + "<hr />");

		if (json.roll >= dc) {
			$(".spell-desc").append("<p>Saving throw succeeded.</p>");
			switchTurn();
		}
		else {
			$(".spell-desc").append("<p>Saving throw failed... Take damage.</p>");
			enableButton(caster, "damage");
		}
	});
}

function switchTurn() {
	disableButton(caster, "ALL");

	var temp = caster;
	caster = target;
	target = temp;

	$("#arrow-" + target).removeClass("on");
	$("#arrow-" + caster).addClass("on");

	enableButton(caster, "cast");
}