#Password Manager

Kişisel parolalarınızı güvenli, düzenli ve erişilebilir şekilde yönetmenizi sağlayan bir ASP.NET MVC projesidir.

## Proje Hakkında

**Password Manager**, kullanıcıların kendi hesaplarını oluşturabildiği ve özel şifrelerini güvenli şekilde saklayabildiği bir web uygulamasıdır. Kullanıcı dostu arayüzü ve güçlü şifreleme altyapısı ile modern bir parola yönetimi deneyimi sunar.

##  Kullanılan Teknolojiler

- **ASP.NET MVC** (Razor View Engine)
- **Entity Framework Core** (Code First yaklaşımı)
- **BCrypt** – Kullanıcı şifrelerinin güvenli bir şekilde hash'lenmesi
- **AES Şifreleme** – Parolaların veri tabanında şifreli olarak saklanması
- **Bootstrap** – Responsive ve modern arayüz tasarımı

##  Özellikler
-  Kullanıcı kayıt ve giriş sistemi  
-  Parola ekleme, listeleme, düzenleme ve silme  
-  Arama ve kategori filtreleme  
-  Kopyalama işlemlerinde güvenlik kontrolü  
-  Kategorilere göre parola gruplama  
-  AES ile veri şifreleme, BCrypt ile kullanıcı doğrulama

---

##  Demo

Demo videosu ektedir. (Alternatif olarak: [YouTube’da izle](#))

---

##  Kurulum

1. Bu repoyu klonlayın:
    ```bash
    git clone https://github.com/dilexgit/PasswordManager.git
    ```
2. Visual Studio ile açın.

3. `appsettings.json` ya da `Web.config` dosyasındaki veritabanı bağlantı bilgisini düzenleyin.

4. Migration oluşturmak için (Entity Framework Core):
    ```bash
    Add-Migration InitialCreate
    Update-Database
    ```
5. Uygulamayı çalıştırın (`Ctrl + F5`)


## Dosya Yapısı
PasswordManager/
│

├── Controllers/ → Account ve Passwords işlemleri

├── Models/ → EF Core Entity tanımları

├── Services/ → Kullanıcı ve parola işlemleri (bağımsız servis yapısı)

├── Views/ → Razor View dosyaları

├── Utilities/ → AES ve BCrypt gibi yardımcı sınıflar

└── wwwroot/ → Bootstrap, JS, CSS içerikleri
