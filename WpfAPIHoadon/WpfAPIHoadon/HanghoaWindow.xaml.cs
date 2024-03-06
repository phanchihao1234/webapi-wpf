using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using WpfAPIHoadon.Models;

namespace WpfAPIHoadon
{
    /// <summary>
    /// Interaction logic for HanghoaWindow.xaml
    /// </summary>
    public partial class HanghoaWindow : Window
    {
        public HanghoaWindow()
        {
            InitializeComponent();
        }
        private void hienthi()
        {
            List<Hanghoa> dsHH = CXulyHanghoa.getDSHanghoa();
            if (dsHH == null)
            {
                MessageBox.Show("Lỗi kết nối dữ liệu!");
            }
            else
            {
                dg.ItemsSource = dsHH;
            }
        }
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            hienthi();

        }

        private void btnThem_Click(object sender, RoutedEventArgs e)
        {
            Hanghoa x = new Hanghoa();
            x.Mahang = txtMahang.Text;
            x.Tenhang= txtTenhang.Text;
            x.Dvt=txtDvt.Text;
            x.Dongia=double.Parse(txtDongia.Text);
            if (CXulyHanghoa.themHanghoa(x)==false)
            {
                MessageBox.Show("Lỗi hệ thống khi thêm!");
            }
            else
            {
                hienthi();
            }

        }

        private void btnXoa_Click(object sender, RoutedEventArgs e)
        {
            string mahang=dg.SelectedValue.ToString();
            if (CXulyHanghoa.xoaHanghoa(mahang) == false)
            {
                MessageBox.Show("Bị lỗi hệ thống khi xóa!");
            }
            else
            {
                hienthi();
            }
        }

        private void btnSua_Click(object sender, RoutedEventArgs e)
        {
            Button? btn = sender as Button;
            FrameworkElement? fe = btn.Parent as FrameworkElement;
            TextBox? txtMH = fe.FindName("txtMahang") as TextBox;
            TextBox? txtTH = fe.FindName("txtTenhang") as TextBox;
            TextBox? txtDVT = fe.FindName("txtDvt") as TextBox;
            TextBox? txtDG = fe.FindName("txtDongia") as TextBox;

            Hanghoa x = new Hanghoa();
            x.Mahang = txtMH.Text;
            x.Tenhang = txtTH.Text;
            x.Dvt = txtDVT.Text;
            x.Dongia = double.Parse(txtDG.Text);

            if(CXulyHanghoa.suaHanghoa(x) == false)
            {
                MessageBox.Show("Lỗi hệ thống khi sửa!");
            }
            else
            {
                hienthi();
            }
        }

        private void dg_LoadingRowDetails(object sender, DataGridRowDetailsEventArgs e)
        {
            FrameworkElement fe = e.DetailsElement;
            Models.Hanghoa? a = e.Row.Item as Models.Hanghoa;
            if (a != null)
            {
                TextBox? txtMH = fe.FindName("txtMahang") as TextBox;
                txtMH.Text = a.Mahang;
                TextBox? txtTH = fe.FindName("txtTenhang") as TextBox;
                txtTH.Text = a.Tenhang;
                TextBox? txtDVT = fe.FindName("txtDvt") as TextBox;
                txtDVT.Text = a.Dvt;
                TextBox? txtDG = fe.FindName("txtDongia") as TextBox;
                txtDG.Text = a.Dongia.ToString();

            }
        }
    }
}
