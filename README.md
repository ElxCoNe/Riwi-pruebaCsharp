#  PruebaRiwi - Sistema de Reservas de Espacios Deportivos

Este proyecto es una aplicación web desarrollada en **C# con ASP.NET Core MVC** como prueba técnica para Riwi. Permite gestionar usuarios, lugares deportivos y reservas a través de una interfaz web.

---

## 🛠️ Tecnologías usadas

- **C#** — Lenguaje principal del proyecto
- **ASP.NET Core MVC** — Framework para aplicaciones web
- **Entity Framework Core** — Para manejar la base de datos sin escribir SQL a mano
- **MySQL** — Base de datos relacional
- **HTML / CSS / JavaScript** — Para las vistas del lado del cliente

---

## 📁 Estructura del proyecto

```
PruebaRiwi/
│
├── Controllers/        
├── Data/               
├── Enums/              
├── Migrations/         
├── Models/             
├── Response/           
├── Services/           
├── Views/              
├── wwwroot/            
│
├── Program.cs          
├── appsettings.json    
└── PruebaRiwi.csproj   
```

---

## ⚙️ ¿Qué hace esta app?

La aplicación gestiona tres entidades principales:

- **Usuarios** — Personas que pueden hacer reservas
- **Lugares Deportivos** — Espacios disponibles para reservar
- **Reservas** — La relación entre un usuario y un lugar en una fecha

---

## ¿Cómo correr el proyecto?

### 1. Requisitos previos

Asegúrate de tener instalado:

- [.NET SDK 8+](https://dotnet.microsoft.com/download)
- [MySQL](https://dev.mysql.com/downloads/)
- Un IDE como [Visual Studio](https://visualstudio.microsoft.com/) o [Rider](https://www.jetbrains.com/rider/)

### 2. Clonar el repositorio

```bash
git clone https://github.com/ElxCoNe/Riwi-pruebaCsharp.git
```

### 3. Configurar la base de datos

Abre el archivo `appsettings.json` y edita la cadena de conexión con tus datos de MySQL:

```json
"ConnectionStrings": {
  "DefaultConnection": "server=localhost;database=prueba_riwi;user=root;password=TU_CONTRASEÑA"
}
```

### 4. Aplicar las migraciones

Esto crea las tablas en tu base de datos automáticamente:

```bash
dotnet ef database update
```

### 5. Correr la aplicación

```bash
dotnet run
```

Luego abre tu navegador en `https://localhost:5001` o la URL que aparezca en la terminal.

---

##  Servicios registrados

En `Program.cs` se registran los siguientes servicios que maneja la app:

| Servicio             | ¿Qué hace?                                  |
|----------------------|---------------------------------------------|
| `UserService`        | Maneja la lógica relacionada a los usuarios |
| `SportPlaceService`  | Maneja los lugares deportivos               |
| `ReservationService` | Maneja las reservas                         |

---

## Patrón de arquitectura

El proyecto sigue el patrón **MVC (Modelo - Vista - Controlador)**:

- **Modelo** → Define los datos (carpeta `Models/`)
- **Vista** → Lo que ve el usuario en pantalla (carpeta `Views/`)
- **Controlador** → Conecta el modelo con la vista (carpeta `Controllers/`)

---

##  Autor

**ElxCoNe**  
Proyecto de la primera prueba en ruta avanzada en C# para [Riwi](https://riwi.io)