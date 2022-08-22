selections = [];
const url = `/Home/Fights`;

$(document).ready(function () {
	$("input").on("click", function () {
		let idFighter = $(this).attr("name").replace("f", "");
		if ($(this)[0].checked)
			selections.push(idFighter)
		else
			selections = arrayRemove(selections, idFighter);
		$(".selection h2")[0].innerText = selections.length + "/20";

		formatSelections();
	});

	$("button").on("click", function () {
		//if (selections.length > 20) {
		//	alert("Precisa selecionar exatamente 20 lutadores");
		//	return;
		//}

		Run();
	});
});

function Run() {
	/*let xhr = new XMLHttpRequest();
	let params = `ids=${selections}`;
	xhr.onreadystatechange = function () {
		if (this.readyState === 4) {
			alert('Funciona');
		}
	}
	xhr.open('post', url);
	xhr.setRequestHeader('Content-type', 'application/x-www-form-urlencoded');
	xhr.send(params);*/

	$.ajax({
		type: 'GET',
		url: url,
		data: {
			ids: selections.toString()
		}
	}).done(function (data) {
		if (!data.success)
			alert(data.message)
		else {
			$("#campeao").html(data.data[0].Nome);
			$("#segundo").html(data.data[1].Nome);
			$("#terceiro").html(data.data[2].Nome);
			$('#mymodal').modal('show');
		}

	});

}

function formatSelections() {
	selections.length > 20
		? $('.selection h2').addClass('erro')
		: $('.selection h2').removeClass('erro');
}

function arrayRemove(a, v) {
	return a.filter(function (va) {
		return va != v;
	});
}