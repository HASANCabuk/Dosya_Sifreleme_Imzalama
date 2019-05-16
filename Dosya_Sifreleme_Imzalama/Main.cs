using System;
using System.IO;
using System.Text;
using System.Windows.Forms;

namespace Dosya_Sifreleme_Imzalama
{
    public partial class Main : Form
    {
        public Main()
        {
            InitializeComponent();
        }
        private string aPath = @".\Users\AA";
        private string bPath = @".\Users\BB";
        private void Main_Load(object sender, EventArgs e)// A ve B kişileri için klasor oluşturma ve public private anahtarlarını yaratma
        {
            Directory.CreateDirectory(aPath);
            Directory.CreateDirectory(bPath);
            Asymmetric_Crypto.generateKeys(aPath + "\\pub.cert", aPath + "\\priv.cert");
            Asymmetric_Crypto.generateKeys(bPath + "\\pub.cert", bPath + "\\priv.cert");
        }
        private OpenFileDialog openFile;
        private FolderBrowserDialog fBrowser;
        private FileInfo fileInfo;
        private string fExtension = "";//dosya uzantısı
        private string extensionTemp = "";// dosya uzantısını elde tutma
        private string fName;
        private byte[] file;
        private string fileName;
        private string[] fpaths;
        private string receiver;//alıcı
        private string senderPub;//göndericinin public anahtar yolu
        private string senderPriv;//göndericinin private anahtar yolu
        private string receiverPub; //alıcının  public anahtar yolu
        private string receiverPriv;//alıcının  private anahtar yolu
        private string userr = "";
        private byte[] outKey;// şifreleme anahtaı
        private byte[] encryptedFile;//şifreli metin
        private byte[] signedFile;//imzalı metin
        private byte[] hashF;//imzalı hash
        private byte[] encryptedSignedHashF;//şifreli imzalı hash
        private bool sc;

        private void dosyaAc_Click(object sender, EventArgs e)// Kullanıcının istegine göre dosya gönderme ve dosya alma işlemleri
        {
            if (user.Text == "A" || user.Text == "B")
            {
                if (dosyaIslem.Text == "Dosya Gönder")
                {
                    if (user.Text == "A")
                    {
                        receiver = bPath;
                        userr = user.Text;
                        senderPub = aPath + "\\pub.cert";
                        senderPriv = aPath + "\\priv.cert";
                        receiverPub = bPath + "\\pub.cert";
                        receiverPriv = bPath + "\\priv.cert";
                    }
                    else
                    {
                        receiver = aPath;
                        userr = user.Text;
                        senderPub = bPath + "\\pub.cert";
                        senderPriv = bPath + "\\priv.cert";
                        receiverPub = aPath + "\\pub.cert";
                        receiverPriv = aPath + "\\priv.cert";
                    }
                    try
                    {
                        openFile = new OpenFileDialog();
                        if (openFile.ShowDialog() == DialogResult.OK)
                        {
                            fileName = openFile.FileName;
                            fileInfo = new FileInfo(fileName);
                            fExtension = fileInfo.Extension;
                            fName = fileInfo.Name;
                            file = File.ReadAllBytes(fileName);

                            if (file == null)
                            {
                                MessageBox.Show("Lütfen veri içeren bir dosya seçiniz");
                            }
                            else
                            {
                                MessageBox.Show("İmzalanacak veya şifrelenecek dosya alındı.");
                                panel1.Visible = true;
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Dosya alınırken hata oluştu. Error:" + ex.Message);
                    }
                }
                else if (dosyaIslem.Text == "Dosya Al")
                {
                    sc = false;
                    fBrowser = new FolderBrowserDialog();
                    if (fBrowser.ShowDialog() == DialogResult.OK)
                    {
                        fpaths = Directory.GetFiles(fBrowser.SelectedPath);

                        for (int i = 0; i < fpaths.Length; i++)
                        {
                            fileInfo = new FileInfo(fpaths[i]);
                            fExtension = fileInfo.Extension;
                            if (fExtension == ".s" || fExtension == ".cr" || fExtension == ".sc")
                            {
                                if (fExtension == ".s")
                                {
                                    signedFile = File.ReadAllBytes(fpaths[i]);
                                    extensionTemp = fExtension;
                                }
                                else
                                {
                                    if (fExtension == ".sc")
                                    {
                                        sc = true;
                                    }
                                    encryptedFile = File.ReadAllBytes(fpaths[i]);
                                    extensionTemp = fExtension;
                                }
                            }
                            else
                            if (fExtension == ".key")
                            {
                                outKey = File.ReadAllBytes(fpaths[i]);
                            }
                            else if (fExtension == ".hash")
                            {
                                hashF = File.ReadAllBytes(fpaths[i]);
                            }
                            else if (fExtension == ".esHash")
                            {
                                encryptedSignedHashF = File.ReadAllBytes(fpaths[i]);
                            }
                            else
                            {
                                file = File.ReadAllBytes(fpaths[i]);
                            }
                        }
                        if (extensionTemp != "")
                        {
                            if (sc)
                            {
                                extensionTemp = ".sc";
                            }
                            if (extensionTemp == ".s")
                            {
                                if (signedFile == null || file == null)
                                {
                                    MessageBox.Show("Klasor dosyalarında eksik var.");
                                }
                                else
                                {
                                    signature.Checked = true;
                                    crypto.Checked = false;
                                    MessageBox.Show("Dosya alma tamamlandı. Dosya sadece imzalı");
                                }
                            }
                            else if (extensionTemp == ".cr")
                            {
                                if (encryptedFile == null||hashF==null||encryptedSignedHashF==null)
                                {
                                    MessageBox.Show("Klasor dosyalarında eksik var.");
                                }
                                else
                                {
                                    signature.Checked = false;
                                    crypto.Checked = true;
                                    MessageBox.Show("Dosya alma tamamlandı. Dosya sadece şifreli");
                                }
                            }
                            else if (extensionTemp == ".sc")
                            {
                                if (signedFile == null || encryptedFile == null || hashF == null || encryptedSignedHashF == null)
                                {
                                    MessageBox.Show("Klasor dosyalarında eksik var.");
                                }
                                else
                                {
                                    signature.Checked = true;
                                    crypto.Checked = true;
                                    MessageBox.Show("Dosya alma tamamlandı. Dosya hem imzalı hem şifreli");
                                }
                            }
                            panel1.Visible = true;
                        }
                        else
                        {
                            MessageBox.Show("Şifreli veya imzalı bir dosya klasorü seçilmemiş");
                        }
                    }
                }
                else
                {
                    MessageBox.Show("Lütfen  dosya işlemi seçiniz.");
                }
            }
            else
            {
                MessageBox.Show("Lütfen kullanıcı seçiniz.");
            }
        }
        private void make_Click(object sender, EventArgs e)
        {
            string path = "";//dosya yolu
            byte[] signedData;// alınan dosyanın imzalı hali
            byte[] key;// şifrelemek için oluşturlan anahtar
            byte[] encrypted;//şifreli metin
            byte[] hash;//  Giden hash
            byte[] signedHash;// Giden imzalı hash
            byte[] encryptedSignedHash;// Giden şifreli imzalı hash
            byte[] decryptedSignedHash;//Çözülmüş imzalı hash
            byte[] encryptedKey;//Giden şifreli anahtar
            byte[] decryptedKey;//Çözülmüş anahtar
            if (dosyaIslem.Text == "Dosya Gönder")
            {
                if (signature.Checked == false && crypto.Checked == false)
                {
                    MessageBox.Show("Lütfen işlem seçiniz");
                }
                else
                {
                    hash = Hashing.getSha512Hash(file);//gönderilecek dosyanın hası alınıyor
                    if (signature.Checked && crypto.Checked == false)//Dosya imzalama
                    {
                        try
                        {
                            path = receiver + "\\" + userr + " Gelen Imzali";
                            Directory.CreateDirectory(path);
                            signedData = Asymmetric_Crypto.signData(file, senderPriv);//Asimetrik kripto sınıfından 
                            File.WriteAllBytes(path + "\\Signed.s", signedData);
                            File.WriteAllBytes(path + "\\" + fName, file);
                            MessageBox.Show("Dosya imzalanıp gönderildi");
                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show("Dosya İmzalamada HATA:", ex.Message);
                        }
                    }
                    else if (signature.Checked == false && crypto.Checked)//Dosya şifreleme
                    {
                        try
                        {
                            path = receiver + "\\" + userr + " Gelen Sifreli";

                            Directory.CreateDirectory(path);
                            signedHash = Asymmetric_Crypto.signData(hash, senderPriv);//imzalı hash                          
                            key = Symmetric_Crypto.generateKey();//key
                            encryptedSignedHash = Symmetric_Crypto.encrypt(signedHash, key);//şifreli imzalı hash
                            encryptedKey = Asymmetric_Crypto.encrypt(key, receiverPub);// Sadece alıcının okuyabilecegi key
                            encrypted = Symmetric_Crypto.encrypt(file, key);//Simetrik sınıfından
                            File.WriteAllBytes(path + "\\Key.key", encryptedKey);
                            File.WriteAllBytes(path + "\\" + fName.Split('.')[0] + ".cr", encrypted);
                            File.WriteAllBytes(path + "\\" + fName.Split('.')[0] + ".esHash", encryptedSignedHash);
                            File.WriteAllBytes(path + "\\" + fName.Split('.')[0] + ".hash", hash);
                            MessageBox.Show("Dosya şifrelenip gönderildi.");
                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show("Dosya şifrelemede HATA:", ex.Message);
                        }
                    }
                    else if (signature.Checked && crypto.Checked)//Dosyayı hem imzalama hem şifreleme
                    {
                        try
                        {
                            path = receiver + "\\" + userr + " Gelen Imzali Sifreli";
                            Directory.CreateDirectory(path);
                            signedData = Asymmetric_Crypto.signData(file, senderPriv);//imzalı dosya

                            signedHash = Asymmetric_Crypto.signData(hash, senderPriv);//imzalı hash                          
                            key = Symmetric_Crypto.generateKey();//key
                            encryptedSignedHash = Symmetric_Crypto.encrypt(signedHash, key);//şifreli imzalı hash
                            encryptedKey = Asymmetric_Crypto.encrypt(key, receiverPub);// Sadece alıcının okuyabilecegi key
                            encrypted = Symmetric_Crypto.encrypt(file, key);//Simetrik sınıfından
                            File.WriteAllBytes(path + "\\Key.key", encryptedKey);
                            File.WriteAllBytes(path + "\\" + fName.Split('.')[0] + ".sc", encrypted);
                            File.WriteAllBytes(path + "\\Signed.s", signedData);
                            File.WriteAllBytes(path + "\\" + fName.Split('.')[0] + ".esHash", encryptedSignedHash);
                            File.WriteAllBytes(path + "\\" + fName.Split('.')[0] + ".hash", hash);
                            MessageBox.Show("Dosya şifrelenip imzalanıp gönderildi.");
                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show("Dosya imzalama ve şifrelemede HATA:", ex.Message);
                        }
                    }
                }
            }
            else if (dosyaIslem.Text == "Dosya Al")
            {
                path = receiver + "\\ Gelen Dosya";
                if (signature.Checked && crypto.Checked == false)//Dosya İmza doğrulama 
                {
                    try
                    {
                        if (Asymmetric_Crypto.verifyData(file, signedFile, senderPub))
                            MessageBox.Show("İmza doğrulandı");
                        else
                        {
                            MessageBox.Show("İmza doğrulanamadı. Gelen klasor siliniyor.");
                            Directory.Delete(receiver + "\\" + userr + " Gelen Imzali", true);
                        }

                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Dosya imza doğrulamada HATA:", ex.Message);

                    }
                }
                else if (signature.Checked == false && crypto.Checked)//Dosya şifre çözme
                {
                    try
                    {
                        Directory.CreateDirectory(path);
                        decryptedKey = Asymmetric_Crypto.decrypt(outKey, receiverPriv);//dosya şifreleme anahtarını sadece alıcı açabilir.                      
                        decryptedSignedHash = Symmetric_Crypto.decrypt(encryptedSignedHashF, decryptedKey);
                        if (Asymmetric_Crypto.verifyData(hashF,decryptedSignedHash, senderPub))//göndericinin public anahtarı ile hashin değişmediği dogrulanıyor
                        {
                            byte[] plainText = Symmetric_Crypto.decrypt(encryptedFile, decryptedKey);
                            File.WriteAllBytes(path + "\\" + fName, plainText);
                        }
                        else
                        {
                            MessageBox.Show("Dosyanın hashi doğrulanamadı. Klasor siliniyor.");
                            Directory.Delete(path, true);
                            Directory.Delete(receiver + "\\" + userr + " Gelen Sifreli", true);
                        }                                         
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Dosya deşifrelemede HATA:", ex.Message);
                    }
                }
                else if (signature.Checked && crypto.Checked)//Dosya şifre çözme ve imza onaylama
                {
                    try
                    {
                        Directory.CreateDirectory(path);
                        decryptedKey = Asymmetric_Crypto.decrypt(outKey, receiverPriv);//dosya şifreleme anahtarını sadece alıcı açabilir.                      
                        decryptedSignedHash = Symmetric_Crypto.decrypt(encryptedSignedHashF, decryptedKey);
                        if (Asymmetric_Crypto.verifyData(hashF, decryptedSignedHash, senderPub))//göndericinin public anahtarı ile hashin değişmediği dogrulanıyor
                        {
                            byte[] plainText = Symmetric_Crypto.decrypt(encryptedFile, decryptedKey);
                            if (Asymmetric_Crypto.verifyData(plainText, signedFile, senderPub))
                            {
                                MessageBox.Show("Dosya deşifrelenip İmza doğrulandı");
                                File.WriteAllBytes(path + "\\" + fName, plainText);
                            }
                            else
                            {
                                MessageBox.Show("Dosya deşifrelendi fakat imza doğrulanamadı. Gelen klasor siliniyor.");
                                Directory.Delete(path, true);
                                Directory.Delete(receiver + "\\" + userr + " Gelen Imzali Sifreli", true);
                            }

                        }
                        else
                        {
                            MessageBox.Show("Dosyanın hashi doğrulanamadı. Klasor siliniyor.");
                            Directory.Delete(path, true);
                            Directory.Delete(receiver + "\\" + userr + " Gelen Imzali Sifreli", true);
                        }                        
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Dosya deşifreleme ve imza doğrulamada HATA:", ex.Message);

                    }

                }
            }

        }
        private void dosyaIslem_SelectedValueChanged(object sender, EventArgs e)
        {
            if (dosyaIslem.Text == "Dosya Gönder")
            {
                signature.Text = "İmzala";
                crypto.Text = "Şifrele";
                signature.Enabled = true;
                crypto.Enabled = true;

            }
            else
            {
                if (user.Text=="A")
                {
                    user.Text = "B";
                }else
                if (user.Text=="B")
                {
                    user.Text = "A";
                }
                signature.Text = "İmzayı Dogrula";
                crypto.Text = "Şifre Çöz";
                signature.Enabled = false;
                crypto.Enabled = false;
            }
            panel1.Visible = false;
        }
    }
}

