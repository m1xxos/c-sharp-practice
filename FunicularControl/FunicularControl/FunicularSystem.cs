using System;
using System.Collections.Generic;
using System.Threading;
using System.Windows.Media;
using System.Windows.Shapes;

namespace FunicularControl
{
    public class FunicularSystem
    {
        private List<Rectangle> indicators;
        private MainWindow window;
        private int state = 0;
        private Timer stateTimer;

        public FunicularSystem(List<Rectangle> indicators, MainWindow window)
        {
            this.indicators = indicators;
            this.window = window;
        }

        public void NextState(object? info = null)
        {
            state = (state + 1) % 4;
            PaintState();
        }

        public void StartAuto()
        {
            stateTimer = new Timer(NextState, null, 1000, 2000);
        }

        public void StopAuto()
        {
            stateTimer?.Dispose();
        }

        private void PaintState()
        {
            window.Dispatcher.Invoke(() =>
            {
                switch (state)
                {
                    case 0: // Ф1 едет
                        indicators[0].Fill = Brushes.Green;
                        indicators[1].Fill = Brushes.White;
                        break;
                    case 1: // оба стоят
                        indicators[0].Fill = Brushes.White;
                        indicators[1].Fill = Brushes.White;
                        break;
                    case 2: // Ф2 едет
                        indicators[0].Fill = Brushes.White;
                        indicators[1].Fill = Brushes.Green;
                        break;
                    case 3: // оба стоят
                        indicators[0].Fill = Brushes.White;
                        indicators[1].Fill = Brushes.White;
                        break;
                }
            });
        }
    }
}
