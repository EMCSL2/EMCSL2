using System.Collections.Generic;
using System.Windows;

namespace EMCSL2;

public partial class Input : Window
{
    public string name = "";
    public string is17 = "";
    public bool success = false;

    public Input()
    {
        InitializeComponent();
        var a = new List<string>
        {
            "是",
            "否"
        };
        版本.ItemsSource = a;
    }

    private void 确定_OnClick(object sender,
        RoutedEventArgs e)
    {
        if (!(名字.Text != "" && !名字.Text.Contains("\\") && !名字.Text.Contains("/") && !名字.Text.Contains("\"")))
        {
            MessageBox.Show("输入的备注名有误，请检查：\n1. 备注名不能为空。\n2. 备注名不能含有特殊字符。");
            名字.Text = "";
            return;
        }

        if (版本.Text == "")
        {
            MessageBox.Show("请选择版本是否大于1.16.5（不含1.16.5）。");
            return;
        }

        name = 名字.Text;
        is17 = (版本.Text == "是"
            ? "1"
            : "0");

        success = true;
        this.Close();
    }
}