using System;
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
		* 函数名		：AddNewPoint()
		* 函数功能		：为chart添加新的点
		* 输入			：timeStamp、ptSeries
		* 输出			：无
		* 作者			：ChristinAdol
		*******************************************************************************/
		private void AddNewPoint(DateTime timeStamp, Series ptSeries)
		{
			//将新数据点添加到其系列中
			ptSeries.Points.AddXY(timeStamp.ToOADate(), rnd.Next(500));

			//删除超过当前时间4min前的点。
			double removePoint = timeStamp.AddMinutes((double)(4) * (-1)).ToOADate();      //设定当前时间4min前的点
			while (ptSeries.Points[0].XValue < removePoint)
			{
				ptSeries.Points.RemoveAt(0);    //将点移除
			}
			chart1.ChartAreas[0].AxisX.Minimum = ptSeries.Points[0].XValue;     //x轴时间最小值
			chart1.ChartAreas[0].AxisX.Maximum = DateTime.FromOADate(ptSeries.Points[0].XValue).AddMinutes(5).ToOADate();    //x轴时间最大值
			chart1.Invalidate();
		}

		/*******************************************************************************
		* 函数名		：timer1_Tick
		* 函数功能		：定时函数
		* 输入			：无
		* 输出			：无
		* 作者			：ChristinAdol
		*******************************************************************************/
		private void timer1_Tick(object sender, EventArgs e)
		{
			timer1.Interval = 1000;     //定时1000ms
			DateTime timeStamp = DateTime.Now;      //获取当前时间到timeStamp
			AddNewPoint(timeStamp, chart1.Series[0]);
		}
	}
}
