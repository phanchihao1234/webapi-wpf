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
    /// Interaction logic for HoadonWindow.xaml
    /// </summary>
    public partial class HoadonWindow : Window
    {
        private Hoadon hd = new Hoadon();
        public HoadonWindow()
        {
            InitializeComponent();
        }
        private void hienthi()
        {
            List<Hanghoa> dsHH = CXulyHanghoa.getDSHanghoa();
            List<Hoadon> dsHD = CXulyHanghoa.getDSHoadon();
            if (dsHH == null || dsHD == null)
            {
                MessageBox.Show("Lỗi kết nối dữ liệu!");
            }
            else
            {
                cmbMahang.ItemsSource = dsHH;
                dgHoadon.ItemsSource= dsHD;
            }
        }
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            hienthi();
        }

        private void btnChonhang_Click(object sender, RoutedEventArgs e)
        {
            if (cmbMahang.SelectedValue == null) return;
            string mahang = cmbMahang.SelectedValue.ToString();

            Hanghoa hh = CXulyHanghoa.getHanghoa(mahang);
            if (hh == null)
            {
                MessageBox.Show("Lỗi đọc dữ liệu từ hệ thống!");
                return;
            }
            Chitiethoadon ct = null;
            foreach (Chitiethoadon a in hd.Chitiethoadons.Where(t => t.Mahang == mahang))
            {
                ct = a;
                break;
            }
            if (ct == null)
            {
                ct = new Chitiethoadon();
                ct.Mahang = mahang;
                ct.MahangNavigation = hh;
                ct.Soluong = int.Parse(txtSoluong.Text);
                ct.Dongia = double.Parse(txtDongia.Text);

                hd.Chitiethoadons.Add(ct);
            }
            else
            {
                ct.Soluong += int.Parse(txtSoluong.Text);
            }

            dgCTHD.ItemsSource = hd.Chitiethoadons.Select(x => new {
                Mahang = x.Mahang,
                Tenhang = x.MahangNavigation.Tenhang,
                Dvt = x.MahangNavigation.Dvt,
                Dongia = x.Dongia,
                Soluong = x.Soluong,
                Thanhtien = x.Soluong * x.Dongia
            }).ToList();

            txtThanhtienhd.Text = hd.Chitiethoadons.Sum(x => x.Soluong * x.Dongia).ToString();
        }

        private void btnLaphd_Click(object sender, RoutedEventArgs e)
        {
            hd.Sohd = txtSohd.Text;
            hd.Ngaylaphd = dpNgaylaphd.SelectedDate;
            hd.Tenkh = txtTenkh.Text;


            foreach (Chitiethoadon ct in hd.Chitiethoadons)
            {
                ct.Sohd = hd.Sohd;
                ct.MahangNavigation = null;
            }

            if (CXulyHanghoa.themHoadon(hd) == false)
            {
                MessageBox.Show("Lỗi hệ thống khi thêm!");
            }
            else
            {
                List<Hoadon> ds = CXulyHanghoa.getDSHoadon();
                if (ds == null)
                {
                    MessageBox.Show("Lỗi hệ thống khi đọc dữ liệu!");
                }
                else
                {
                    dgHoadon.ItemsSource = ds;
                    hd = new Hoadon();

                    dgCTHD.ItemsSource = null;
                }    
            }
            //db.Hoadons.Add(hd);
            //db.SaveChanges();

            //dgHoadon.ItemsSource = db.Hoadons.ToList();

            
        }

        private void dgHoadon_LoadingRowDetails(object sender, DataGridRowDetailsEventArgs e)
        {
            FrameworkElement fe = e.DetailsElement;
            Hoadon hd = e.Row.Item as Hoadon;

            TextBox txtSHD = fe.FindName("txtSohd") as TextBox;
            TextBox txtTKH = fe.FindName("txtTenkh") as TextBox;
            TextBox txtTong = fe.FindName("txtThanhtienhd") as TextBox;
            TextBox txtNgay = fe.FindName("txtNgaylaphd") as TextBox;
            DataGrid dg = fe.FindName("dgCTHD") as DataGrid;

            List<Chitiethoadon> dsCTHD = CXulyHanghoa.getDSChitietHoadon(hd.Sohd);
            List<Hanghoa> dsHH = CXulyHanghoa.getDSHanghoa();
            if(dsCTHD==null || dsHH == null)
            {
                MessageBox.Show("Lỗi đọc dữ liệu từ hệ thống!");
                return;
            }
            txtSHD.Text = hd.Sohd;
            txtTKH.Text = hd.Tenkh;
            txtTong.Text = dsCTHD.Sum(t => t.Soluong * t.Dongia).ToString();
            txtNgay.Text = hd.Ngaylaphd.Value.ToShortDateString();

            dg.ItemsSource = dsCTHD.Join(dsHH, x => x.Mahang, y => y.Mahang, (x, y) => new {
                    Mahang = x.Mahang,
                    Tenhang = y.Tenhang,
                    Dvt = y.Dvt,
                    Dongia = x.Dongia,
                    Soluong = x.Soluong,
                    Thanhtien = x.Soluong * x.Dongia
                }).ToList();

        }
    }
}
