import { Component, OnInit } from '@angular/core';
import { LegendItem, ChartType } from '../lbd/lbd-chart/lbd-chart.component';
import * as Chartist from 'chartist';

@Component({
  selector: 'app-home',
  templateUrl: './home.component.html',
  styleUrls: ['./home.component.css']
})
export class HomeComponent implements OnInit {
    public investmentChartType: ChartType;
    public investmentChartData: any;
    public investmentChartLegendItems: LegendItem[];
    public tasks: any;
    public portfolioChartType: ChartType; // Renomeado de performanceChartType
    public portfolioChartData: any;
    public portfolioChartOptions: any;
    public portfolioChartResponsive: any[];
    public portfolioChartLegendItems: LegendItem[];

    public revenueChartType: ChartType;
    public revenueChartData: any;
    public revenueChartOptions: any;
    public revenueChartResponsive: any[];
    public revenueChartLegendItems: LegendItem[];


    constructor() { }

    ngOnInit() {
        // Gráfico de Investimentos por Categoria
        this.investmentChartType = ChartType.Pie;
        this.investmentChartData = {
          labels: ['40%', '35%', '25%'],
          series: [40, 35, 25]
        };
        this.investmentChartLegendItems = [
          { title: 'Renda Fixa', imageClass: 'fa fa-circle text-info' },
          { title: 'Renda Variável', imageClass: 'fa fa-circle text-success' },
          { title: 'Fundos', imageClass: 'fa fa-circle text-warning' }
        ];

        // Gráfico de Performance da Carteira (agora chamado de Portfolio)
        this.portfolioChartType = ChartType.Line; // Alterado de performanceChartType
        this.portfolioChartData = {
          labels: ['Jan', 'Feb', 'Mar', 'Apr', 'Mai', 'Jun', 'Jul', 'Ago', 'Set', 'Out', 'Nov', 'Dez'],
          series: [
            [5000, 7000, 7500, 9000, 10500, 12000, 13500, 14500, 16000, 17500, 19000, 20000], // Carteira Toro
            [4800, 6800, 7200, 8700, 10200, 11500, 13000, 14000, 15500, 17000, 18500, 19500]  // Ibovespa
          ]
        };
        this.portfolioChartOptions = {
          low: 4000,
          high: 22000,
          showArea: true,
          height: '245px',
          axisX: {
            showGrid: false,
          },
          lineSmooth: Chartist.Interpolation.simple({
            divisor: 3
          }),
          showLine: true,
          showPoint: true,
        };
        this.portfolioChartResponsive = [
          ['screen and (max-width: 640px)', {
            axisX: {
              labelInterpolationFnc: function (value) {
                return value[0];
              }
            }
          }]
        ];
        this.portfolioChartLegendItems = [
          { title: 'Carteira Toro', imageClass: 'fa fa-circle text-info' },
          { title: 'Ibovespa', imageClass: 'fa fa-circle text-danger' }
        ];

        // Gráfico de Receita por Mês
        this.revenueChartType = ChartType.Bar;
        this.revenueChartData = {
          labels: ['Jan', 'Fev', 'Mar', 'Abr', 'Mai', 'Jun', 'Jul', 'Ago', 'Set', 'Out', 'Nov', 'Dez'],
          series: [
            [1000, 1500, 2000, 2500, 3200, 4000, 4600, 5000, 5500, 6000, 7000, 8000], // Receita
            [500, 700, 1100, 1300, 1800, 2200, 2600, 2900, 3200, 3500, 4000, 4500] // Despesas
          ]
        };
        this.revenueChartOptions = {
          seriesBarDistance: 10,
          axisX: {
            showGrid: false
          },
          height: '245px'
        };
        this.revenueChartResponsive = [
          ['screen and (max-width: 640px)', {
            seriesBarDistance: 5,
            axisX: {
              labelInterpolationFnc: function (value) {
                return value[0];
              }
            }
          }]
        ];
        this.revenueChartLegendItems = [
          { title: 'Receita', imageClass: 'fa fa-circle text-success' },
          { title: 'Despesas', imageClass: 'fa fa-circle text-danger' }
        ];
    }
}
