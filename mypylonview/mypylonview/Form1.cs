using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using PylonC.NETSupportLibrary;

namespace mypylonview
{
    public partial class Form1 : Form
    {
        private ImageProvider m_imageProvider = new ImageProvider();/* 創建一個圖像提供變數 */
        private Bitmap m_bitmap = null;/* 圖像暫存檔變數 */

        public Form1()
        {
            InitializeComponent();
            /* 建立m_imageProvider所需要的環境建置 */
            m_imageProvider.GrabErrorEvent += new ImageProvider.GrabErrorEventHandler(OnGrabErrorEventCallback);
            m_imageProvider.DeviceRemovedEvent += new ImageProvider.DeviceRemovedEventHandler(OnDeviceRemovedEventCallback);
            m_imageProvider.DeviceOpenedEvent += new ImageProvider.DeviceOpenedEventHandler(OnDeviceOpenedEventCallback);
            m_imageProvider.DeviceClosedEvent += new ImageProvider.DeviceClosedEventHandler(OnDeviceClosedEventCallback);
            m_imageProvider.GrabbingStartedEvent += new ImageProvider.GrabbingStartedEventHandler(OnGrabbingStartedEventCallback);
            m_imageProvider.ImageReadyEvent += new ImageProvider.ImageReadyEventHandler(OnImageReadyEventCallback);
            m_imageProvider.GrabbingStoppedEvent += new ImageProvider.GrabbingStoppedEventHandler(OnGrabbingStoppedEventCallback);

            UpdateDeviceList();
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            /* Stops the grabbing of images. */
            Stop();
            /* Close the image provider. */
            CloseTheImageProvider();
        }
        /* Handles the event related to the occurrence of an error while grabbing proceeds. */
        private void OnGrabErrorEventCallback(Exception grabException, string additionalErrorMessage)
        {
            if (InvokeRequired)
            {
                /* If called from a different thread, we must use the Invoke method to marshal the call to the proper thread. */
                BeginInvoke(new ImageProvider.GrabErrorEventHandler(OnGrabErrorEventCallback), grabException, additionalErrorMessage);
                return;
            }
            ShowException(grabException, additionalErrorMessage);
        }

        /* Handles the event related to the removal of a currently open device. */
        private void OnDeviceRemovedEventCallback()
        {
            if (InvokeRequired)
            {
                /* If called from a different thread, we must use the Invoke method to marshal the call to the proper thread. */
                BeginInvoke(new ImageProvider.DeviceRemovedEventHandler(OnDeviceRemovedEventCallback));
                return;
            }
            /* Stops the grabbing of images. */
            Stop();
            /* Close the image provider. */
            CloseTheImageProvider();
        }

        /* Handles the event related to a device being open. */
        private void OnDeviceOpenedEventCallback()
        {
            if (InvokeRequired)
            {
                /* If called from a different thread, we must use the Invoke method to marshal the call to the proper thread. */
                BeginInvoke(new ImageProvider.DeviceOpenedEventHandler(OnDeviceOpenedEventCallback));
                return;
            }
        }

        /* Handles the event related to a device being closed. */
        private void OnDeviceClosedEventCallback()
        {
            if (InvokeRequired)
            {
                /* If called from a different thread, we must use the Invoke method to marshal the call to the proper thread. */
                BeginInvoke(new ImageProvider.DeviceClosedEventHandler(OnDeviceClosedEventCallback));
                return;
            }
        }

        /* Handles the event related to the image provider executing grabbing. */
        private void OnGrabbingStartedEventCallback()
        {
            if (InvokeRequired)
            {
                /* If called from a different thread, we must use the Invoke method to marshal the call to the proper thread. */
                BeginInvoke(new ImageProvider.GrabbingStartedEventHandler(OnGrabbingStartedEventCallback));
                return;
            }
        }

        /* Handles the event related to an image having been taken and waiting for processing. */
        private void OnImageReadyEventCallback()
        {
            if (InvokeRequired)
            {
                /* If called from a different thread, we must use the Invoke method to marshal the call to the proper thread. */
                BeginInvoke(new ImageProvider.ImageReadyEventHandler(OnImageReadyEventCallback));
                return;
            }

            try
            {
                /* Acquire the image from the image provider. Only show the latest image. The camera may acquire images faster than images can be displayed*/
                ImageProvider.Image image = m_imageProvider.GetLatestImage();

                /* Check if the image has been removed in the meantime. */
                if (image != null)
                {
                    /* Check if the image is compatible with the currently used bitmap. */
                    if (BitmapFactory.IsCompatible(m_bitmap, image.Width, image.Height, image.Color))
                    {
                        /* Update the bitmap with the image data. */
                        BitmapFactory.UpdateBitmap(m_bitmap, image.Buffer, image.Width, image.Height, image.Color);
                        /* To show the new image, request the display control to update itself. */
                        pictureBox.Refresh();
                    }
                    else /* A new bitmap is required. */
                    {
                        BitmapFactory.CreateBitmap(out m_bitmap, image.Width, image.Height, image.Color);
                        BitmapFactory.UpdateBitmap(m_bitmap, image.Buffer, image.Width, image.Height, image.Color);
                        /* We have to dispose the bitmap after assigning the new one to the display control. */
                        Bitmap bitmap = pictureBox.Image as Bitmap;
                        /* Provide the display control with the new bitmap. This action automatically updates the display. */
                        pictureBox.Image = m_bitmap;
                        if (bitmap != null)
                        {
                            /* Dispose the bitmap. */
                            bitmap.Dispose();
                        }
                    }
                    /* The processing of the image is done. Release the image buffer. */
                    m_imageProvider.ReleaseImage();
                    /* The buffer can be used for the next image grabs. */
                }
            }
            catch (Exception e)
            {
                ShowException(e, m_imageProvider.GetLastErrorMessage());
            }
        }

        /* Handles the event related to the image provider having stopped grabbing. */
        private void OnGrabbingStoppedEventCallback()
        {
            if (InvokeRequired)
            {
                /* If called from a different thread, we must use the Invoke method to marshal the call to the proper thread. */
                BeginInvoke(new ImageProvider.GrabbingStoppedEventHandler(OnGrabbingStoppedEventCallback));
                return;
            }
        }

        /* 停止影像供應程序並處理異常 */
        private void Stop()
        {
            /* Stop the grabbing. */
            try
            {
                m_imageProvider.Stop();
            }
            catch (Exception e)
            {
                ShowException(e, m_imageProvider.GetLastErrorMessage());
            }
        }

        /* 關閉映像提供程序並處理異常。 */
        private void CloseTheImageProvider()
        {
            /* Close the image provider. */
            try
            {
                m_imageProvider.Close();
            }
            catch (Exception e)
            {
                ShowException(e, m_imageProvider.GetLastErrorMessage());
            }
        }

        /*開始抓取一個圖像並處理異常。 */
        private void OneShot()
        {
            try
            {
                m_imageProvider.OneShot(); /* Starts the grabbing of one image. */
            }
            catch (Exception e)
            {
                ShowException(e, m_imageProvider.GetLastErrorMessage());
            }
        }

        /* 開始抓取圖像，直到抓取停止並處理異常。 */
        private void ContinuousShot()
        {
            try
            {
                m_imageProvider.ContinuousShot(); /* Start the grabbing of images until grabbing is stopped. */
            }
            catch (Exception e)
            {
                ShowException(e, m_imageProvider.GetLastErrorMessage());
            }
        }

        /* 在消息框中顯示異常。 */
        private void ShowException(Exception e, string additionalErrorMessage)
        {
            string more = "\n\nLast error message (may not belong to the exception):\n" + additionalErrorMessage;
            MessageBox.Show("Exception caught:\n" + e.Message + (additionalErrorMessage.Length > 0 ? more : ""), "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
        private void btnOneshot_Click(object sender, EventArgs e)
        {
            OneShot();
        }

        private void btnContinous_Click(object sender, EventArgs e)
        {
            ContinuousShot();
        }

        private void btnStop_Click(object sender, EventArgs e)
        {
            Stop();
        }
        private void UpdateDeviceList()
        {
            try
            {
                /* 向設備列舉器詢問設備列表。 */
                List<DeviceEnumerator.Device> list = DeviceEnumerator.EnumerateDevices();

                /* 將每個新設備添加到列表中。 */
                foreach (DeviceEnumerator.Device device in list)
                {
                    ListViewItem item = new ListViewItem(device.Name);
                    if (device.Tooltip.Length > 0)
                    {
                        item.ToolTipText = device.Tooltip;
                    }
                    item.Tag = device;

                    //******//
                    try
                    {
                        // 使用設備數據中的索引打開圖像提供者。
                        m_imageProvider.Open(device.Index);
                    }
                    catch (Exception e)
                    {
                        ShowException(e, m_imageProvider.GetLastErrorMessage());
                    }
                    //*******//
                }
            }
            catch (Exception e)
            {
                ShowException(e, m_imageProvider.GetLastErrorMessage());
            }
        }
    }
}
