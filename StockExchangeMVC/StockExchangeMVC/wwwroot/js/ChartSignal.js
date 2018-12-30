$(function () {

    $.ajax({
        url: "/Signal/SignalsMonth",
        data: { json: true },
        type: "GET",
        dataType: "json"
    }).done(function (result) {

        var chartsDetails = document.querySelectorAll("div.candle-charts-details");
        console.log(chartsDetails);
        console.log(result);

        for (let i = 0; i < chartsDetails.length; i++) {
            getChartsDetails(result[i], chartsDetails[i].id);
        }

    }
    ).fail(function (xhr, status, err) { }
    ).always(function (xhr, status) { }
    );

});

function getChartsDetails(dataChart, id) {

    google.charts.load('current', { 'packages': ['corechart'] });
    google.charts.setOnLoadCallback(drawChart);

    var chartData = [];
    chartData.push(["srednia", dataChart.monthTickBefore.low, dataChart.monthTickBefore.open, dataChart.monthTickBefore.close, dataChart.monthTickBefore.high]);
    //chartData.push(["srednia", dataChart.monthTick.low, dataChart.monthTick.low, dataChart.monthTick.low + dataChart.avarageRange, dataChart.monthTick.low + dataChart.avarageRange]);

    chartData.push(["aktualny", dataChart.monthTick.low, dataChart.monthTick.open, dataChart.monthTick.close, dataChart.monthTick.high]);


    for (let i = 0; i < dataChart.dayTicksTable.length; i++) {

        chartData.push([
            dataChart.dayPercentRange[i],
            dataChart.dayTicksTable[i].low,
            dataChart.dayTicksTable[i].open,
            dataChart.dayTicksTable[i].close,
            dataChart.dayTicksTable[i].high
        ]);
    }

    function drawChart() {
        var data = google.visualization.arrayToDataTable(
            chartData
            //nazwa, low | open | close | high
            , true);

        var options = {
            legend: 'none',
            backgroundColor: 'EBE4DC',
            hAxis: { title: "Years", direction: 1, slantedText: true, slantedTextAngle: 60 },
            candlestick: {
                fallingColor: { strokeWidth: 0, fill: '#E96C7C' }, // red
                risingColor: { strokeWidth: 0, fill: '#99C1B0' }   // green
            }
        };

        var chart = new google.visualization.CandlestickChart(document.getElementById(id));

        chart.draw(data, options);
        
    }

}
