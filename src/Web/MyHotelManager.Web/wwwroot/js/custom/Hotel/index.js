function startTimeSofia() {
    var today = moment.tz("Europe/Sofia");
    document.getElementById('displayMomentSofia').innerHTML = today.format('HH:mm:ss');
    document.getElementById('displayJsDateSofia').innerHTML = today.format('DD/MM/YYYY');
    var t = setTimeout(startTimeSofia, 500);
}

startTimeSofia();

function startTimeLondon() {
    var today = moment.tz("Europe/London");
    document.getElementById('displayMomentLondon').innerHTML = today.format('HH:mm:ss');
    document.getElementById('displayJsDateLondon').innerHTML = today.format('DD/MM/YYYY');
    var t = setTimeout(startTimeLondon, 500);
}

startTimeLondon();

function startTimeMoscow() {
    var today = moment.tz("Europe/Moscow");
    document.getElementById('displayMomentMoscow').innerHTML = today.format('HH:mm:ss');
    document.getElementById('displayJsDateMoscow').innerHTML = today.format('DD/MM/YYYY');
    var t = setTimeout(startTimeMoscow, 500);
}

startTimeMoscow();

function AreaChart(JanuaryReservations,
    FebruaryReservations,
    MarchReservations,
    AprilReservations,
    MayReservations,
    JuneReservations,
    JulyReservations,
    AugustReservations,
    SeptemberReservations,
    OctoberReservations,
    NovemberReservations,
    DecemberReservations) {
    var ctx = document.getElementById("AreaChart");
    var myLineChart = new Chart(ctx,
        {
            type: 'line',
            data: {
                labels: [
                    "January", "February", "March", "April", "May", "June", "July", "August", "September", "October",
                    "November", "December"
                ],
                datasets: [
                    {
                        label: "Reservations",
                        lineTension: 0.3,
                        backgroundColor: "rgba(78, 115, 223, 0.05)",
                        borderColor: "rgba(78, 115, 223, 1)",
                        pointRadius: 3,
                        pointBackgroundColor: "rgba(78, 115, 223, 1)",
                        pointBorderColor: "rgba(78, 115, 223, 1)",
                        pointHoverRadius: 3,
                        pointHoverBackgroundColor: "rgba(78, 115, 223, 1)",
                        pointHoverBorderColor: "rgba(78, 115, 223, 1)",
                        pointHitRadius: 10,
                        pointBorderWidth: 2,
                        data: [JanuaryReservations,
                            FebruaryReservations,
                            MarchReservations,
                            AprilReservations,
                            MayReservations,
                            JuneReservations,
                            JulyReservations,
                            AugustReservations,
                            SeptemberReservations,
                            OctoberReservations,
                            NovemberReservations,
                            DecemberReservations
                        ],
                    }
                ],
            },
            options: {
                maintainAspectRatio: false,
                layout: {
                    padding: {
                        left: 10,
                        right: 25,
                        top: 25,
                        bottom: 0
                    }
                },
                scales: {
                    xAxes: [
                        {
                            time: {
                                unit: 'date'
                            },
                            gridLines: {
                                display: true,
                                drawBorder: false
                            },
                            ticks: {
                                maxTicksLimit: 12
                            }
                        }
                    ],
                    yAxes: [
                        {
                            ticks: {
                                maxTicksLimit: 5,
                                padding: 10,
                                // Include a dollar sign in the ticks
                                callback: function (value, index, values) {
                                    return number_format(value);
                                }
                            },
                            gridLines: {
                                color: "rgb(234, 236, 244)",
                                zeroLineColor: "rgb(234, 236, 244)",
                                drawBorder: false,
                                borderDash: [2],
                                zeroLineBorderDash: [2]
                            }
                        }
                    ],
                },
                legend: {
                    display: false
                },
                tooltips: {
                    backgroundColor: "rgb(255,255,255)",
                    bodyFontColor: "#858796",
                    titleMarginBottom: 10,
                    titleFontColor: '#6e707e',
                    titleFontSize: 14,
                    borderColor: '#dddfeb',
                    borderWidth: 1,
                    xPadding: 15,
                    yPadding: 15,
                    displayColors: true,
                    intersect: true,
                    mode: 'index',
                    caretPadding: 10,
                    callbacks: {
                        label: function (tooltipItem, chart) {
                            var datasetLabel = chart.datasets[tooltipItem.datasetIndex].label || '';
                            return datasetLabel + ': ' + number_format(tooltipItem.yLabel);
                        }
                    }
                }
            }
        });
}

function BarChart(twoYearEarly,
    oneYearEarly,
    thisYear,
    ReservationsCountForTwoYearEarly,
    ReservationsCountForOneYearEarly,
    ReservationsCountForThisYear) {
    var ctx = document.getElementById("BarChart");
    var myBarChart = new Chart(ctx, {
        type: 'bar',
        data: {
            labels: [twoYearEarly, oneYearEarly, thisYear],
    datasets: [{
        label: "Reservations",
        backgroundColor: "#4e73df",
        hoverBackgroundColor: "#2e59d9",
        borderColor: "#4e73df",
        data: [ReservationsCountForTwoYearEarly, ReservationsCountForOneYearEarly, ReservationsCountForThisYear]
}]
            },
options: {
    maintainAspectRatio: false,
        layout: {
        padding: {
            left: 10,
                right: 25,
                    top: 25,
                        bottom: 0
        }
    },
    scales: {
        xAxes: [{
            time: {
                unit: 'year'
            },
            gridLines: {
                display: true,
                drawBorder: true
            },
            ticks: {
                maxTicksLimit: 10
            },
            maxBarThickness: 40,
        }],
            yAxes: [{
                ticks: {
                    maxTicksLimit: 5,
                    padding: 10,
                    // Include a dollar sign in the ticks
                    callback: function (value, index, values) {
                        return number_format(value);
                    }
                },
                gridLines: {
                    color: "rgb(234, 236, 244)",
                    zeroLineColor: "rgb(234, 236, 244)",
                    drawBorder: true,
                    borderDash: [2],
                    zeroLineBorderDash: [2]
                }
            }],
                },
    legend: {
        display: false
    },
    tooltips: {
        titleMarginBottom: 10,
            titleFontColor: '#6e707e',
                titleFontSize: 14,
                    backgroundColor: "rgb(255,255,255)",
                        bodyFontColor: "#858796",
                            borderColor: '#dddfeb',
                                borderWidth: 1,
                                    xPadding: 15,
                                        yPadding: 15,
                                            displayColors: true,
                                                caretPadding: 10,
                                                    callbacks: {
            label: function(tooltipItem, chart) {
                var datasetLabel = chart.datasets[tooltipItem.datasetIndex].label || '';
                return datasetLabel + ': ' + number_format(tooltipItem.yLabel);
            }
        }
    },
}
        });
}

function PieChart(OccupiedRoomsCount, AvailableRoomsCount) {
    var ctx = document.getElementById("PieChart");
    var myPieChart = new Chart(ctx, {
        type: 'doughnut',
        data: {
            labels: ["Occupied", "Empty"],
            datasets: [{
                data: [OccupiedRoomsCount, AvailableRoomsCount],
                backgroundColor: ['#FF4646', '#5AEAA5'],
                hoverBackgroundColor: ['#C53636', '#4DC28A'],
                hoverBorderColor: "rgba(234, 236, 244, 1)"
            }]
        },
        options: {
            maintainAspectRatio: false,
            tooltips: {
                backgroundColor: "rgb(255,255,255)",
                bodyFontColor: "#11069C",
                borderColor: '#dddfeb',
                borderWidth: 1,
                xPadding: 15,
                yPadding: 15,
                displayColors: true,
                caretPadding: 10
            },
            legend: {
                display: true
            },
            cutoutPercentage: 10
        }
    });
}