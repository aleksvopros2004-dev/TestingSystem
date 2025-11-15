// TestingSystem.WindowsForms/EditUserForm.cs
using TestingSystem.Core.Models;
using TestingSystem.Data.Repositories;

namespace TestingSystem.WindowsForms;

public partial class EditUserForm : Form
{
    private readonly IUserRepository _userRepository;
    private readonly User _user;

    public event EventHandler? UserUpdated;

    public EditUserForm(IUserRepository userRepository, User user)
    {
        _userRepository = userRepository;
        _user = user;
        InitializeComponent();
        LoadUserData();
    }

    private void InitializeComponent()
    {
        this.Text = $"Редактирование пользователя: {_user.FullName}";
        this.Size = new Size(400, 250);
        this.StartPosition = FormStartPosition.CenterParent;
        this.FormBorderStyle = FormBorderStyle.FixedDialog;
        this.MaximizeBox = false;

        CreateControls();
    }

    private void CreateControls()
    {
        // Логин
        var lblLogin = new Label
        {
            Text = "Логин:",
            Location = new Point(20, 20),
            Size = new Size(100, 20)
        };

        var txtLogin = new TextBox
        {
            Location = new Point(130, 20),
            Size = new Size(200, 20),
            Name = "txtLogin"
        };

        // ФИО
        var lblFullName = new Label
        {
            Text = "ФИО:",
            Location = new Point(20, 50),
            Size = new Size(100, 20)
        };

        var txtFullName = new TextBox
        {
            Location = new Point(130, 50),
            Size = new Size(200, 20),
            Name = "txtFullName"
        };

        // Роль
        var lblRole = new Label
        {
            Text = "Роль:",
            Location = new Point(20, 80),
            Size = new Size(100, 20)
        };

        var cmbRole = new ComboBox
        {
            Location = new Point(130, 80),
            Size = new Size(150, 20),
            DropDownStyle = ComboBoxStyle.DropDownList,
            Name = "cmbRole"
        };
        cmbRole.Items.AddRange(new object[] { UserRole.User, UserRole.Admin });

        // Активность
        var chkActive = new CheckBox
        {
            Text = "Активный",
            Location = new Point(20, 110),
            Size = new Size(100, 20),
            Name = "chkActive"
        };

        // Кнопки
        var btnSave = new Button
        {
            Text = "Сохранить",
            Location = new Point(130, 150),
            Size = new Size(100, 30),
            Name = "btnSave"
        };
        btnSave.Click += BtnSave_Click;

        var btnCancel = new Button
        {
            Text = "Отмена",
            Location = new Point(240, 150),
            Size = new Size(80, 30)
        };
        btnCancel.Click += (s, e) => this.Close();

        // Сообщение
        var lblMessage = new Label
        {
            Location = new Point(20, 190),
            Size = new Size(350, 20),
            TextAlign = ContentAlignment.MiddleCenter,
            Name = "lblMessage",
            ForeColor = Color.Red
        };

        this.Controls.AddRange(new Control[]
        {
            lblLogin, txtLogin,
            lblFullName, txtFullName,
            lblRole, cmbRole,
            chkActive,
            btnSave, btnCancel,
            lblMessage
        });
    }

    private void LoadUserData()
    {
        var txtLogin = this.Controls.Find("txtLogin", true).FirstOrDefault() as TextBox;
        var txtFullName = this.Controls.Find("txtFullName", true).FirstOrDefault() as TextBox;
        var cmbRole = this.Controls.Find("cmbRole", true).FirstOrDefault() as ComboBox;
        var chkActive = this.Controls.Find("chkActive", true).FirstOrDefault() as CheckBox;

        if (txtLogin != null) txtLogin.Text = _user.Login;
        if (txtFullName != null) txtFullName.Text = _user.FullName;
        if (cmbRole != null) cmbRole.SelectedItem = _user.Role;
        if (chkActive != null) chkActive.Checked = _user.IsActive;
    }

    private async void BtnSave_Click(object? sender, EventArgs e)
    {
        var txtLogin = this.Controls.Find("txtLogin", true).FirstOrDefault() as TextBox;
        var txtFullName = this.Controls.Find("txtFullName", true).FirstOrDefault() as TextBox;
        var cmbRole = this.Controls.Find("cmbRole", true).FirstOrDefault() as ComboBox;
        var chkActive = this.Controls.Find("chkActive", true).FirstOrDefault() as CheckBox;
        var lblMessage = this.Controls.Find("lblMessage", true).FirstOrDefault() as Label;
        var btnSave = this.Controls.Find("btnSave", true).FirstOrDefault() as Button;

        if (txtLogin == null || txtFullName == null || cmbRole == null ||
            chkActive == null || lblMessage == null || btnSave == null)
            return;

        // Валидация
        var login = txtLogin.Text.Trim();
        var fullName = txtFullName.Text.Trim();
        var role = (UserRole)cmbRole.SelectedItem;

        if (string.IsNullOrEmpty(login) || string.IsNullOrEmpty(fullName))
        {
            lblMessage.Text = "Логин и ФИО обязательны для заполнения";
            return;
        }

        // Блокируем кнопку
        btnSave.Enabled = false;
        lblMessage.Text = "Сохранение...";
        lblMessage.ForeColor = Color.Blue;

        try
        {
            // Обновляем пользователя
            _user.Login = login;
            _user.FullName = fullName;
            _user.Role = role;
            _user.IsActive = chkActive.Checked;

            var success = await _userRepository.UpdateAsync(_user);

            if (success)
            {
                lblMessage.Text = "Пользователь успешно обновлен!";
                lblMessage.ForeColor = Color.Green;

                await Task.Delay(1000);
                UserUpdated?.Invoke(this, EventArgs.Empty);
                this.Close();
            }
            else
            {
                lblMessage.Text = "Ошибка при обновлении пользователя";
                lblMessage.ForeColor = Color.Red;
            }
        }
        catch (Exception ex)
        {
            lblMessage.Text = $"Ошибка: {ex.Message}";
            lblMessage.ForeColor = Color.Red;
        }
        finally
        {
            btnSave.Enabled = true;
        }
    }
}