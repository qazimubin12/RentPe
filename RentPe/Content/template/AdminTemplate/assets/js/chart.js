  //bar
  $(document).ready(function() { 
   var ctx = document.getElementById("myChart").getContext('2d');

var original = Chart.defaults.global.legend.onClick;
Chart.defaults.global.legend.onClick = function(e, legendItem) {
  update_caption(legendItem);
  original.call(this, e, legendItem);
};

var myChart = new Chart(ctx, {
  type: 'bar',
  data: {
    labels: ["M", "T", "W", "T", "F", "S", "S"],
    datasets: [{
      label: 'Expenses',
      backgroundColor: "#009efb",
      data: [12, 19, 3, 17, 28, 24, 7],
    }, {
      label: 'Incomes',
      backgroundColor: "#7bb13c",
      data: [30, 29, 5, 5, 20, 3, 10],
    }]
  }
});

  // gradient line chart
  if ($("#linechart").length) {
    var chart = document.getElementById('linechart').getContext('2d'),
      gradient = chart.createLinearGradient(0, 0, 0, 150);

    gradient.addColorStop(0, 'rgba(0, 158, 251, 0.78)');
    gradient.addColorStop(0.5, 'rgba(255, 255, 255, 0.2)');
    gradient.addColorStop(1, 'rgba(255, 255, 255, 0.1)');

    var data = {
      labels: ["", "M", "", "T", "", "W", "", "T", "", "F", "", "S", "", "S"],
      datasets: [{
        label: 'Customers',
        backgroundColor: gradient,
        pointBackgroundColor: 'white',
        borderWidth: 2,
        borderColor: '#009efb',
        data: [30,35, 20, 25, 40, 30, 20, 25, 35, 40, 45, 43, 18, 40],
      }]
    };

    var options = {
      responsive: true,
      maintainAspectRatio: true,
      animation: {
        easing: 'easeInOutQuad',
        duration: 520
      },
      scales: {
        xAxes: [{
          gridLines: {
            lineWidth: 1,
            drawBorder: false,
          }
        }],
        yAxes: [{
          gridLines: {
            display: true,
            drawBorder: false
          },
          ticks: {
            display: true,
            beginAtZero: true
          }
        }]
      },
      elements: {
        line: {
          tension: 0.4
        }
      },
      legend: {
        display: false
      },
      point: {
        backgroundColor: 'white'
      },
    };

    var chartInstance = new Chart(chart, {
      type: 'line',
      data: data,
      options: options
    });
  }
   });
