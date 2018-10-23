﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;

namespace Chart_test
{
	public partial class Form1 : Form
	{


		private DateTime minTime, maxTime;

		Random rnd = new Random();      //随机数

		public Form1()
		{
			InitializeComponent();

	
		}

		/*******************************************************************************
		* 函数名		：initchart()
		* 函数功能		：chart初始化
		* 输入			：无
		* 输出			：无
		* 作者			：christinadol
		*******************************************************************************/
		private void InitChart()
		{
			minTime = DateTime.Now;             //x轴最小时间
			maxTime = minTime.AddSeconds(1);    //x轴最大时间

			chart1.ChartAreas[0].AxisX.Minimum = minTime.ToOADate();     //x轴最小值
			chart1.ChartAreas[0].AxisX.Maximum = maxTime.ToOADate();    //x轴最大值
			chart1.ChartAreas[0].AxisY.Minimum = 0;     //y轴最小值
			chart1.ChartAreas[0].AxisY.Maximum = 500;   //y轴最大值

		}

		/*******************************************************************************
		* 函数名		：InitChart()
		* 函数功能		：为chart添加新的点
		* 输入			：timeStamp、ptSeries
		* 输出			：无
		* 作者			：ChristinAdol
		*******************************************************************************/
		private void AddNewPoint(DateTime timeStamp, Series ptSeries)
		{
			//将新数据点添加到其系列中
			ptSeries.Points.AddXY(timeStamp.ToOADate(),rnd.Next(500));

			//删除超过4min的点。
			double removeBefore = timeStamp.AddMinutes((double)(4) * (-1)).ToOADate();		//设定4min前的点的值
			while(ptSeries.Points[0].XValue < removeBefore)
			{
				ptSeries.Points.RemoveAt(0);	//将点移除
			}
			chart1.ChartAreas[0].AxisX.Minimum = ptSeries.Points[0].XValue;     //x轴时间最小值
			chart1.ChartAreas[0].AxisX.Maximum = DateTime.FromOADate(ptSeries.Points[0].XValue).AddMinutes(5).ToOADate();    //x轴时间最大值

			chart1.Invalidate();

		}

		//定时器刷新图表
		private void timer1_Tick(object sender, EventArgs e)
		{
			timer1.Interval = 1000;     //定时1000ms
			DateTime timeStamp = DateTime.Now;		//获取当前时间到timeStamp
			AddNewPoint(timeStamp, chart1.Series[0]);		
		}
	}
}
