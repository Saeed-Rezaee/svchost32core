using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace svchost32core
{
    public partial class svchostform : Form
    {
        private readonly Timer _timer;
        private int _xCoord, _yCoord, _maxXCoord, _maxYCoord, _minXCoord, _minYCoord, _xDirection = 1, _yDirection = 1;

        public svchostform()
        {
            InitializeComponent();
            _timer = new Timer
            {
                Interval = 10
            };
            _timer.Tick += TimerTick;

            this.MouseMove += Svchostform_MouseMove;
            this.Load += Svchostform_Load;
        }
        private void TimerTick(object sender, EventArgs e)
        {

            if (_xDirection == 1)
            {

                if (_xCoord < _maxXCoord)
                {
                    _xCoord++;

                }
                else if (_xCoord == _maxXCoord)
                {
                    if (_yDirection == 1 && _yCoord < _maxYCoord)
                    {
                        _yCoord++;
                    }
                    else
                    {
                        _xDirection = -1;
                        _yDirection = -1;
                    }
                }


            }
            else if (_xDirection == -1)
            {

                if (_xCoord > _minXCoord)
                {
                    _xCoord--;

                }
                else if (_xCoord == _minXCoord)
                {

                    if (_yDirection == -1 && _yCoord > _minXCoord)
                    {
                        _yCoord--;
                    }
                    else
                    {
                        _yDirection = 1;
                        _xDirection = 1;
                        _xCoord = _minXCoord;
                        _yCoord = _minYCoord;
                    }

                }
            }
            InvokeMouseActivity(_xCoord, _yCoord);
        }

        private void Svchostform_Load(object sender, EventArgs e)
        {
            try
            {
                _maxXCoord = int.Parse(ConfigurationManager.AppSettings["MaximumXCoordinate"]);
                _maxYCoord = int.Parse(ConfigurationManager.AppSettings["MaximumYCoordinate"]);
                _minXCoord = int.Parse(ConfigurationManager.AppSettings["MinimumXCoordinate"]);
                _minYCoord = int.Parse(ConfigurationManager.AppSettings["MinimumYCoordinate"]);
                _xCoord = _minXCoord;
                _yCoord = _minYCoord;
                _timer.Start();

            }
            catch (Exception ex)
            {
                _maxXCoord = 2000;
                _maxYCoord = 800;
                Trace.WriteLine(ex);
            }
        }

        private void Svchostform_MouseMove(object sender, MouseEventArgs e)
        {
            Trace.WriteLine($"X:{e.X}, Y:{e.Y}");
        }

        private void InvokeMouseActivity(int x, int y)
        {
            try
            {
                Trace.WriteLine($"Moving cursor to ({x},{y})");
                MouseActivity.MoveCursorPosition(x, y);
                //MouseActivity.mouse_event((uint)MouseActivity.MouseFlags.MOUSEEVENTF_MOVE|(uint)MouseActivity.MouseFlags.MOUSEEVENTF_ABSOLUTE, (uint)x, (uint)y, 0, 0);
            }
            catch (Exception ex)
            {
                Trace.WriteLine(ex);
            }
        }
    }
}
