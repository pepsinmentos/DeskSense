var fChart;
var occupancyLatest;
var occupancyData;
var occupancyLabels;

window.onload = function ()
{
    console.log("vanilla js ftw");
    fillImage();
   loadChart();

  
  
}

function loadChart() {
    var xhttp = new XMLHttpRequest();
    xhttp.onreadystatechange = function () {
        if (this.readyState == 4 && this.status == 200) {
            occupancyData = JSON.parse(xhttp.responseText);
            console.log(occupancyData);
            renderFusionChart();
            //renderChart();
           // setTimeout(loadChart, 5000);
        } else {
           // console.error(xhttp.res);
        }
    };

    var getUrl = window.location;
    var baseUrl = getUrl.protocol + "//" + getUrl.host + "/"
    xhttp.open("GET", baseUrl + "api/Area/OccupancyLive", true);
    xhttp.send();  
}

 function fillImage() {
    var c = document.getElementById("myCanvas");
    var ctx = c.getContext("2d");
    var image = new Image(60, 45);

    image.src = "https://s3.eu-west-2.amazonaws.com/pepsinmentos/offic_layout.jpg";
    var self = this;
    image.onload = function () {
        c.width = this.naturalWidth;
        c.height = this.naturalHeight;

        ctx.drawImage(this, 0, 0);
        self.refreshOccupancy();
    };

}

 function refreshOccupancy() { 
     var xhttp = new XMLHttpRequest();
     xhttp.onreadystatechange = function () {
         if (this.readyState == 4 && this.status == 200) {
             occupancyLatest = JSON.parse(xhttp.responseText);
             drawOccupancy();
             setTimeout(refreshOccupancy, 5000);
         } else {
             //console.error(xhttp.response);
         }
     };

     var getUrl = window.location;
     var baseUrl = getUrl.protocol + "//" + getUrl.host + "/"
     xhttp.open("GET",baseUrl + "api/Area/OccupancyLatest", true);
     xhttp.send();
}

    function drawOccupancy() {
    var c = document.getElementById("myCanvas");
    var ctx = c.getContext("2d");

    for (var i = 0; i < this.occupancyLatest.length; i++) {
        var occupancy = this.occupancyLatest[i];
        fillDesk(occupancy.x,
            occupancy.y,
            occupancy.occupied,
            ctx);

    }
}

    function fillDesk(x, y, occupied, ctx) {
        ctx.fillStyle = occupied ? "green" : "red";
        ctx.beginPath();
        ctx.arc(x, y, 20, 0, Math.PI * 2);
        ctx.closePath();
        ctx.fill();
    }



////// ---------------------------------- //////////////
function renderChart() {
    /* Add a basic data series with six labels and values */
    var series = [];
    for (var i = 0; i < occupancyData.length; i++) {
        series.push(occupancyData[i].data);
    }
    var data = {
        labels: ['1', '2', '3', '4', '5', '6'],
        series:series
    };

    /* Set some base options (settings will override the default settings in Chartist.js *see default settings*). We are adding a basic label interpolation function for the xAxis labels. */
    var options = {
        axisX: {
            labelInterpolationFnc: function (value) {
                return 'Calendar Week ' + value;
            }
        }
    };

    /* Now we can specify multiple responsive settings that will override the base settings based on order and if the media queries match. In this example we are changing the visibility of dots and lines as well as use different label interpolations for space reasons. */
    var responsiveOptions = [
        ['screen and (min-width: 641px) and (max-width: 1024px)', {
            showPoint: false,
            axisX: {
                labelInterpolationFnc: function (value) {
                    return 'Week ' + value;
                }
            }
        }],
        ['screen and (max-width: 640px)', {
            showLine: false,
            axisX: {
                labelInterpolationFnc: function (value) {
                    return 'W' + value;
                }
            }
        }]
    ];

    /* Initialize the chart with the above settings */
    new Chartist.Line('#my-chart', data, options, responsiveOptions);
    }

////////////////////////////-----------------------------/////////////////////////////
function getData(data) {
    var res = [];
    for (var i = 0; i < data.length; i++) {
        res.push({ value: data[i] });
    }
    return res;
}
function renderFusionChart() {
    var series = [];
    for (var i = 0; i < occupancyData.length; i++) {
        series.push({ seriesname: occupancyData[i].label, data: getData(occupancyData[i].data) });
    }
    console.log(series);
    FusionCharts.ready(function () {
        fChart = new FusionCharts({
            type: 'msline',
            renderAt: 'my-chart',
            width: '600',
            height: '400',
            dataFormat: 'json',
            dataSource: {
                "chart": {
                    "caption": "Occupancy",
                    "subcaption": "Displayed per sensor, average and total",
                    "linethickness": "2",                    
                    "showvalues": "0",
                    "formatnumberscale": "1",
                    "labeldisplay": "ROTATE",
                    "slantlabels": "1",
                    "divLineAlpha": "40",
                    "anchoralpha": "0",
                    "animation": "1",
                    "legendborderalpha": "20",
                    "drawCrossLine": "1",
                    "crossLineColor": "#0d0d0d",
                    "crossLineAlpha": "100",
                    "tooltipGrayOutColor": "#80bfff",
                    "theme": "zune"
                },
               "categories": [{
                    "category": [{
                        "label": "1"
                    }, {
                        "label": "2"
                    }, {
                        "label": "3"
                    }, {
                        "label": "4"
                    }, {
                        "label": "5"
                    }, {
                        "label": "6"
                    }, {
                        "label": "7"
                    }, {
                        "label": "8"
                    }, {
                        "label": "9"
                    }, {
                        "label": "10"
                    }, {
                        "label": "11"
                    }, {
                        "label": "12"
                    }, {
                        "label": "13"
                    }, {
                        "label": "14"
                    }, {
                        "label": "15"
                    }, {
                        "label": "16"
                    }, {
                        "label": "17"
                    }, {
                        "label": "18"
                    }, {
                        "label": "19"
                    }, {
                        "label": "20"
                    }]
                }],
                "dataset": series /* [{
                    "seriesname": "Samsung",
                    "data": [{
                        "value": "716000"
                    }, {
                        "value": "771700"
                    }, {
                        "value": "687800"
                    }, {
                        "value": "698300"
                    }, {
                        "value": "826100"
                    }, {
                        "value": "938300"
                    }, {
                        "value": "892800"
                    }, {
                        "value": "904300"
                    }, {
                        "value": "979600"
                    }, {
                        "value": "1069600"
                    }, {
                        "value": "1006600"
                    }, {
                        "value": "1075300"
                    }, {
                        "value": "1170500"
                    }, {
                        "value": "1192100"
                    }, {
                        "value": "1100500"
                    }, {
                        "value": "974200"
                    }, {
                        "value": "936200"
                    }, {
                        "value": "979900"
                    }, {
                        "value": "887400"
                    }, {
                        "value": "1020600"
                    }]
                }, {
                    "seriesname": "Nokia",
                    "data": [{
                        "value": "1174600"
                    }, {
                        "value": "1222800"
                    }, {
                        "value": "1075600"
                    }, {
                        "value": "978700"
                    }, {
                        "value": "1053500"
                    }, {
                        "value": "1117000"
                    }, {
                        "value": "831600"
                    }, {
                        "value": "834200"
                    }, {
                        "value": "823000"
                    }, {
                        "value": "850500"
                    }, {
                        "value": "632200"
                    }, {
                        "value": "609500"
                    }, {
                        "value": "630600"
                    }, {
                        "value": "635800"
                    }, {
                        "value": "496900"
                    }, {
                        "value": "438100"
                    }, {
                        "value": "431300"
                    }, {
                        "value": "330000"
                    }, {
                        "value": "276900"
                    }, {
                        "value": "302900"
                    }]
                }, {
                    "seriesname": "Apple",
                    "data": [{
                        "value": "134800"
                    }, {
                        "value": "160100"
                    }, {
                        "value": "168800"
                    }, {
                        "value": "196300"
                    }, {
                        "value": "173000"
                    }, {
                        "value": "354600"
                    }, {
                        "value": "331200"
                    }, {
                        "value": "289400"
                    }, {
                        "value": "246200"
                    }, {
                        "value": "434600"
                    }, {
                        "value": "383300"
                    }, {
                        "value": "319000"
                    }, {
                        "value": "303300"
                    }, {
                        "value": "502200"
                    }, {
                        "value": "430600"
                    }, {
                        "value": "353500"
                    }, {
                        "value": "381900"
                    }, {
                        "value": "601800"
                    }, {
                        "value": "480900"
                    }, {
                        "value": "460600"
                    }]
                }]*/
            }
        })
            .render();
    });
}