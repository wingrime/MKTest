using System;
using System.Collections.Generic;
using System.Globalization;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Media;

namespace TestingUI.AutoTests
{
    /// <summary>
    /// Interaction logic for AutoTest.xaml
    /// </summary>
    /// 
    public partial class AutoTest : Page
    {
        public AutoTest()
        {
            InitializeComponent();
            var items = new List<TestItem>();
            items.Add(new TestItem() { TestName = "Проверка 1", TestStatus = TestStatus.UnDone });
            items.Add(new TestItem() { TestName = "Проверка 2", TestStatus = TestStatus.UnDone });
            items.Add(new TestItem() { TestName = "Проверка 3", TestStatus = TestStatus.UnDone });
            items.Add(new TestItem() { TestName = "Проверка 4", TestStatus = TestStatus.UnDone });
            items.Add(new TestItem() { TestName = "Проверка 5", TestStatus = TestStatus.UnDone });
            items.Add(new TestItem() { TestName = "Проверка 6", TestStatus = TestStatus.UnDone });
            items.Add(new TestItem() { TestName = "Проверка 7", TestStatus = TestStatus.UnDone });
            items.Add(new TestItem() { TestName = "Проверка 8", TestStatus = TestStatus.UnDone });
            items.Add(new TestItem() { TestName = "Проверка 9", TestStatus = TestStatus.UnDone });
            items.Add(new TestItem() { TestName = "Проверка 6", TestStatus = TestStatus.UnDone });
            items.Add(new TestItem() { TestName = "Проверка 7", TestStatus = TestStatus.UnDone });
            items.Add(new TestItem() { TestName = "Проверка 8", TestStatus = TestStatus.Fail });
            items.Add(new TestItem() { TestName = "Проверка 9", TestStatus = TestStatus.Success });
            items.Add(new TestItem() { TestName = "Проверка 6", TestStatus = TestStatus.Success });
            items.Add(new TestItem() { TestName = "Проверка 7", TestStatus = TestStatus.UnDone });
            items.Add(new TestItem() { TestName = "Проверка 8", TestStatus = TestStatus.Fail });
            items.Add(new TestItem() { TestName = "Проверка 9", TestStatus = TestStatus.Success });
            items.Add(new TestItem() { TestName = "Проверка 6", TestStatus = TestStatus.Success });
            items.Add(new TestItem() { TestName = "Проверка 7", TestStatus = TestStatus.UnDone });
            items.Add(new TestItem() { TestName = "Проверка 8", TestStatus = TestStatus.Fail });
            items.Add(new TestItem() { TestName = "Проверка 9", TestStatus = TestStatus.Success });

            testList.ItemsSource = items;

        }

        private void btnStart_Click(object sender, RoutedEventArgs e)
        {

        }
        private void Hyperlink_Click(object sender, RoutedEventArgs e)
        {
            this.NavigationService.GoBack();
        }
    }
    public class TestItem {
        public string TestName { get; set; }
        public TestStatus TestStatus { get; set; }


    }
    public enum TestStatus {
        UnDone,
        Fail,
        Success
    }
    [ValueConversion(typeof(TestStatus), typeof(Brush))]
    public class ColorStatusConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            switch ((TestStatus)value) {
                case TestStatus.UnDone:
                    return (object)new SolidColorBrush(Color.FromRgb(0xD1, 0xC4, 0xE9));
                case TestStatus.Fail:
                    return (object)new SolidColorBrush(Color.FromRgb(0xF5,0x7F,0x17));
                case TestStatus.Success:
                    return (object)new SolidColorBrush(Color.FromRgb(0x64,0xDD,0x17));
            }
            return null;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
    [ValueConversion(typeof(TestStatus), typeof(string))]
    public class TextStatusConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            switch ((TestStatus)value)
            {
                case TestStatus.UnDone:
                    return "ОЖИДАНИЕ";
                case TestStatus.Fail:
                    return "ПРОВАЛ";
                case TestStatus.Success:
                    return "УСПЕХ";
            }
            return null;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
