/*!
 * Start Bootstrap - Creative Bootstrap Theme (http://startbootstrap.com)
 * Code licensed under the Apache License v2.0.
 * For details, see http://www.apache.org/licenses/LICENSE-2.0.
 */

(function ($) {
    "use strict"; // Start of use strict
    var availableVendors = [
     "ABC",
     "WS Retail",
     "CloudTail",
     "MobicWon",
     "Python",
     "Ruby",
     "Scala",
     "Scheme"
    ];
    var twitterhtml;
    var alchemypositivehtml;
    var alchemynegativehtml;
    var alchemyneutralhtml;
    // jQuery for page scrolling feature - requires jQuery Easing plugin
    $('a.page-scroll').bind('click', function (event) {
        var $anchor = $(this);
        $('html, body').stop().animate({
            scrollTop: ($($anchor.attr('href')).offset().top - 50)
        }, 1250, 'easeInOutExpo');
        event.preventDefault();
    });
    $('#container').hide();
    $("#analyzeBtn").click(function (e) {
        e.preventDefault();
        $('.wrapper').hide();
        $('#container').show();
        $("#vendorName").autocomplete({
            source: availableVendors
        });
        google.charts.load('current', { packages: ['corechart', 'line'] });
        $.ajax({
            type: "GET",
            url: "/Home/TwitterAPIData",
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            cache: false,
            success: function (data) {
               
                google.charts.setOnLoadCallback(drawLineColors);

                function drawLineColors() {
                    var newsdata = new google.visualization.DataTable();
                    newsdata.addColumn('number', 'X');
                    newsdata.addColumn('number', 'Positive');
                    newsdata.addColumn('number', 'Negative');

                    newsdata.addRows([
                      [0, 0, 0], [1, 10, 5], [2, 23, 15], [3, 17, 9], [4, 18, 10], [5, 9, 5],
                      [6, 11, 3], [7, 27, 19], [8, 33, 25], [9, 40, 32], [10, 32, 24], [11, 35, 27],
                      [12, 30, 22], [13, 40, 32], [14, 42, 34], [15, 47, 39], [16, 44, 36], [17, 48, 40],
                      [18, 52, 44], [19, 54, 46], [20, 42, 34], [21, 55, 47], [22, 56, 48], [23, 57, 49],
                      [24, 60, 52], [25, 50, 42], [26, 52, 44], [27, 51, 43], [28, 49, 41], [29, 53, 45],
                      [30, 55, 47], [31, 60, 52], [32, 61, 53], [33, 59, 51], [34, 62, 54], [35, 65, 57],
                      [36, 62, 54], [37, 58, 50], [38, 55, 47], [39, 61, 53], [40, 64, 56], [41, 65, 57],
                      [42, 63, 55], [43, 66, 58], [44, 67, 59], [45, 69, 61], [46, 69, 61], [47, 70, 62],
                      [48, 72, 64], [49, 68, 60], [50, 66, 58], [51, 65, 57], [52, 67, 59], [53, 70, 62],
                      [54, 71, 63], [55, 72, 64], [56, 73, 65], [57, 75, 67], [58, 70, 62], [59, 68, 60],
                      [60, 64, 56], [61, 60, 52], [62, 65, 57], [63, 67, 59], [64, 68, 60], [65, 69, 61],
                      [66, 70, 62], [67, 72, 64], [68, 75, 67], [69, 80, 72]
                    ]);

                    var options = {
                        hAxis: {
                            title: 'Time'
                        },
                        vAxis: {
                            title: 'Repuatation'
                        },
                        colors: ['#a52714', '#097138']
                    };

                    var chart = new google.visualization.LineChart(document.getElementById('newsdatadiv'));
                    chart.draw(newsdata, options);
                }
                //$.map(data, function (item) {
                //    twitterhtml = twitterhtml + "<p>" + item.city + " " + item.Country + " " + item.Evidence + " " + item.polarity + " " + item.PostedTime + " " + item.body + "</p><br/>",
                //    console.log(twitterhtml);
                //});
                //$('#twitterdatadiv').append(twitterhtml);
            },
            error: function () {

            }
        });
        $.ajax({
            type: "GET",
            url: "/Home/TwitterAPIData",
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            cache: false,
            success: function (data) {
                twitterhtml = '<table>';
                $.map(data, function (item) {
                    
                    twitterhtml = twitterhtml + "<tr>" + "<td>" + item.city == null ? "" : item.city + "</td>"
                        + "<td>" + item.Country == null ? "" : item.Country + "</td>"
                        + "<td>" + item.Evidence == null ? "" : item.Evidence + "</td>"
                        + "<td>" + item.polarity == null ? "" : item.polarity + "</td>"
                        + "<td>" + item.PostedTime == null ? "" : item.PostedTime + "</td>"
                        + "<td>" + item.body == null ? "" : item.body + "</td>"
                        "</tr>"
                });
                twitterhtml=twitterhtml+'</table>'
                $('#twitterdatadiv').append(twitterhtml);
            },
            error: function () {

            }
        });
        $.ajax({
            type: "GET",
            url: "/Home/AlchemyAPIPositiveData",
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            cache: false,
            success: function (data) {
                google.charts.setOnLoadCallback(drawChart);
                function drawChart() {
                    var positivedata = google.visualization.arrayToDataTable([
                      ['Effort', 'Amount given'],
                      ['Positive%', 70],
                    ]);

                    var options = {
                        pieHole: 0.7,
                        pieSliceTextStyle: {
                            color: 'black',
                        },
                        legend: 'none'
                    };

                    var chart = new google.visualization.PieChart(document.getElementById('positivedatadiv'));
                    chart.draw(positivedata, options);
                }
            },
            error: function () {

            }
        });
        $.ajax({
            type: "GET",
            url: "/Home/AlchemyAPINegativeeData",
            contentType: "application/json; charset=utf-8",
            cache: false,
            dataType: "json",
            success: function (data) {
                google.charts.setOnLoadCallback(drawChart);
                function drawChart() {
                    var negativedata = google.visualization.arrayToDataTable([
                    ['Effort', 'Amount given'],
                    ['Negative%', 20],
                    ]);

                    var options = {
                        pieHole: 0.7,
                        pieSliceTextStyle: {
                            color: 'black',
                        },
                        legend: 'none'
                    };

                    var chart = new google.visualization.PieChart(document.getElementById('negativedatadiv'));
                    chart.draw(negativedata, options);
                }
            },

            //$.map(data, function (item) {
            //    alchemynegativehtml = alchemynegativehtml + "<p>" + item.Title + " " + item.TimeStamp + " " + item.Id + " " + item.source + " " + item.url + " " + item.Relevence + "</p><br/>",
            //    console.log(alchemynegativehtml);
            //});
            //$('#negativedatadiv').append(alchemynegativehtml);

            error: function () {

            }
        });
        $.ajax({
            type: "GET",
            url: "/Home/AlchemyAPINeutralData",
            contentType: "application/json; charset=utf-8",
            cache: false,
            dataType: "json",
            success: function (data) {
                google.charts.setOnLoadCallback(drawChart);
                function drawChart() {

                    var positivedata = google.visualization.arrayToDataTable([
                      ['Effort', 'Amount given'],
                      ['Neutral%', 10],
                    ]);

                    var options = {
                        pieHole: 0.7,
                        pieSliceTextStyle: {
                            color: 'black',
                        },
                        legend: 'none'
                    };

                    var chart = new google.visualization.PieChart(document.getElementById('neutraldatadiv'));
                    chart.draw(positivedata, options);
                }
                //$.map(data, function (item) {
                //    alchemynegativehtml = alchemynegativehtml + "<p>" + item.Title + " " + item.TimeStamp + " " + item.Id + " " + item.source + " " + item.url + " " + item.Relevence + "</p><br/>",
                //    console.log(alchemynegativehtml);
                //});
                //$('#negativedatadiv').append(alchemynegativehtml);
            },
            error: function () {

            }
        });

    });
   
    // Highlight the top nav as scrolling occurs
    $('body').scrollspy({
        target: '.navbar-fixed-top',
        offset: 51
    })

    // Closes the Responsive Menu on Menu Item Click
    $('.navbar-collapse ul li a').click(function () {
        $('.navbar-toggle:visible').click();
    });

    // Fit Text Plugin for Main Header
    $("h1").fitText(
        1.2, {
            minFontSize: '35px',
            maxFontSize: '65px'
        }
    );

    // Offset for Main Navigation
    $('#mainNav').affix({
        offset: {
            top: 100
        }
    })

    // Initialize WOW.js Scrolling Animations
    new WOW().init();

})(jQuery); // End of use strict
