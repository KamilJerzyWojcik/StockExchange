$(function () {

    var name = document.getElementById("name-item");

    $.ajax({
        url: "/Show/ShowMonths",
        data: { name: name.innerText, json: true },
        type: "GET",
        dataType: "json"
    }).done(function (result) {

        var charts = document.querySelectorAll("div.candle-charts");
        for (let i = 0; i < charts.length - 3; i++) {
            getCharts(result[i], result[i + 1], result[i + 2], result[i + 3], charts[i].id);
        }

        var chartsDetails = document.querySelectorAll("div.candle-charts-details");
        for (let i = 0; i < chartsDetails.length - 3; i++) {
            getChartsDetails(result[i], result[i + 1], result[i + 2], result[i + 3], chartsDetails[i].id);
        }

    }
    ).fail(function (xhr, status, err) { }
    ).always(function (xhr, status) { }
    );

});


function getChartsDetails(dataChart, beforeChartData1, beforeChartData2, beforeChartData3, id) {

    google.charts.load('current', { 'packages': ['corechart'] });
    google.charts.setOnLoadCallback(drawChart);
    var avgRange = (beforeChartData1.range + beforeChartData2.range + beforeChartData3.range) / 3;

    var chartData = [];
    chartData.push(["srednia", dataChart.low, dataChart.low, dataChart.low + avgRange, dataChart.low + avgRange]);

    var dayRangeSum = 0;
    var min = 0;
    var max = 0;
    for (let i = 0; i < dataChart.dayTicksTable.body.length; i++) {

        if (i === 0) {
            max = dataChart.dayTicksTable.body[i].high;
            min = dataChart.dayTicksTable.body[i].low;
        }
        else {
            if (dataChart.dayTicksTable.body[i].high > max) {
                max = dataChart.dayTicksTable.body[i].high;
            }

            if (dataChart.dayTicksTable.body[i].low < min) {
                min = dataChart.dayTicksTable.body[i].low;
            }
        }

        dayRangeSum = max - min;

        chartData.push([
            Math.round(100 * dayRangeSum / avgRange) + "%" + "(" + (i + 1) +")",
            dataChart.dayTicksTable.body[i].low,
            dataChart.dayTicksTable.body[i].open,
            dataChart.dayTicksTable.body[i].close,
            dataChart.dayTicksTable.body[i].high
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
            candlestick: {
                fallingColor: { strokeWidth: 0, fill: '#E96C7C' }, // red
                risingColor: { strokeWidth: 0, fill: '#99C1B0' }   // green
            }
        };

        var chart = new google.visualization.CandlestickChart(document.getElementById(id));

        chart.draw(data, options);


    }

}

function getCharts(dataChart, beforeChartData1, beforeChartData2, beforeChartData3, id) {


    google.charts.load('current', { 'packages': ['corechart'] });
    google.charts.setOnLoadCallback(drawChart);
    var avgRange = (beforeChartData1.range + beforeChartData2.range + beforeChartData3.range) / 3;



    function drawChart() {
        var data = google.visualization.arrayToDataTable([
            //nazwa, low | open | close | high
            ["mies -3", dataChart.low, dataChart.low, dataChart.low + beforeChartData3.range, dataChart.low + beforeChartData3.range],
            ["mies -2", dataChart.low, dataChart.low, dataChart.low + beforeChartData2.range, dataChart.low + beforeChartData2.range],
            ["mies -1", dataChart.low, dataChart.low, dataChart.low + beforeChartData1.range, dataChart.low + beforeChartData1.range],
            ["srednia", dataChart.low, dataChart.low, dataChart.low + avgRange, dataChart.low + avgRange],
            ["Obcenie", dataChart.low, dataChart.open, dataChart.close, dataChart.high]
        ], true);

        var options = {
            legend: 'none',
            backgroundColor: 'EBE4DC',
            candlestick: {
                fallingColor: { strokeWidth: 0, fill: '#E96C7C' }, // red
                risingColor: { strokeWidth: 0, fill: '#99C1B0' }   // green
            }
        };

        var chart = new google.visualization.CandlestickChart(document.getElementById(id));

        chart.draw(data, options);


    }

}