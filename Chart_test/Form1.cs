using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Chart_test
{
	public partial class Form1 : Form
	{
		int i;
		public Form1()
		{
			InitializeComponent();
			chart1.ChartAreas[0].AxisX.Minimum = 0;     //x轴最小值
			chart1.ChartAreas[0].AxisX.Maximum = 20;    //x轴最大值
			chart1.ChartAreas[0].AxisY.Minimum = 0;     //y轴最小值
			chart1.ChartAreas[0].AxisY.Maximum = 500;   //y轴最大值
			i = 0;
		}
		Random rnd = new Random();      //随机数

		//定时器刷新图表
		private void timer1_Tick(object sender, EventArgs e)
		{
			timer1.Interval = 1000;     //定时1000ms
			double newX, newY;
			newX = i;
			newY = rnd.Next(500);
			chart1.Series[0].Points.AddXY(newX, newY);
			i++;
			if (chart1.Series[0].Points.Count > 20)// 绘画坐标点超过100个时将实时更新X时间坐标/
			{
				chart1.ChartAreas[0].AxisX.Minimum = i - 10;//设置X轴最小值
				chart1.ChartAreas[0].AxisX.Maximum = i + 10;//设置X轴最大值
				chart1.ChartAreas[0].AxisX.Interval = 1;//设置每个刻度的跨度
			}
		}
	}
}
