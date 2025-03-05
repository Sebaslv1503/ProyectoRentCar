# Proyecto de Sistema de Renta de Vehículos

## Descripción del Proyecto

Este proyecto es una aplicación web desarrollada en **ASP.NET Core MVC** utilizando **Razor Pages**. El sistema permite a los usuarios gestionar la renta de vehículos, realizar reservas, pagar servicios, y administrar vehículos, clientes y empleados. 

El sistema está diseñado para ofrecer una experiencia de uso intuitiva tanto para los clientes como para los empleados.

## Funcionalidades Clave

- **Administración de Vehículos:** Registro, edición, eliminación y visualización de vehículos disponibles.
- **Gestión de Reservas:** Creación, confirmación, cancelación y visualización de reservas.
- **Administración de Usuarios:** Registro, autenticación y gestión de empleados y clientes.
- **Gestión de Pagos:** Registro y visualización de pagos realizados por los clientes.
- **Información y Estadísticas:** Visualización de estadísticas sobre la cantidad de vehículos y clientes registrados.

## Estructura del Proyecto

El proyecto está organizado en diferentes capas para separar responsabilidades y facilitar la mantenibilidad:

### 1. CapaDatos

Contiene las clases de acceso a datos (**DAL - Data Access Layer**), que interactúan directamente con la base de datos.

#### **Clases Principales:**
- **CadenaDAL:** Gestiona la conexión a la base de datos.
- **ReservaDAL:** Maneja operaciones relacionadas con las reservas.
- **PagoDAL:** Gestiona operaciones relacionadas con los pagos.
- **SeguroDAL:** Maneja operaciones sobre los seguros.
- **UsuarioDAL:** Gestiona operaciones sobre los usuarios.
- **VehiculoDAL:** Gestiona operaciones sobre los vehículos.

### 2. CapaEntidad

Contiene las clases que representan los objetos de negocio del sistema.

#### **Clases Principales:**
- **PagoCLS:** Representa los pagos realizados.
- **SeguroCLS:** Representa los seguros contratados.
- **ReservaCLS:** Representa las reservas de vehículos.
- **UsuarioCLS:** Representa los usuarios del sistema.
- **VehiculoCLS:** Representa los vehículos disponibles para la renta.

### 3. CapaNegocio

Contiene las clases de negocio (**BL - Business Logic Layer**) que encapsulan la lógica de negocio del sistema.

#### **Clases Principales:**
- **PagoBL:** Maneja operaciones de pagos.
- **ReservaBL:** Maneja operaciones de reservas.
- **SeguroBL:** Maneja operaciones de seguros.
- **UsuarioBL:** Maneja operaciones de usuarios.
- **VehiculoBL:** Maneja operaciones de vehículos.

### 4. Capa Presentación

Contiene vistas y controladores en **ASP.NET Core MVC**.

#### **Controladores Principales:**
- **HomeController**: Gestión de la página de inicio.
- **ClienteController**: Funcionalidades para clientes.
- **EmpleadoController**: Funcionalidades para empleados.
- **PagoController**: Gestión de pagos.
- **ReservaController**: Gestión de reservas.
- **SeguroController**: Gestión de seguros.
- **UsuarioController**: Registro e inicio de sesión.
- **VehiculoController**: Gestión de vehículos.

### 5. Scripts y Funciones JavaScript

Funciones JavaScript para interacciones dinámicas.

#### **Funciones Principales:**
- **fetchGet:** Solicitudes GET asincrónicas.
- **fetchPost:** Solicitudes POST asincrónicas.
- **pintar:** Genera tablas HTML dinámicamente.
- **generarTabla:** Estructura de tabla HTML.
- **agregarVehiculo:** Agrega nuevos vehículos.
- **modificarVehiculo:** Modifica vehículos existentes.
- **listarReservas:** Lista reservas de vehículos.
- **confirmarReserva:** Confirma reservas.
- **devolverVehiculo:** Marca reservas como canceladas.
- **listarSeguros:** Lista seguros registrados.
- **listarEmpleados:** Lista empleados registrados.
- **listarClientes:** Lista clientes registrados.
- **obtenerEstadisticas:** Muestra estadísticas del sistema.

### 6. Plantillas de Vista

Las plantillas de vista están organizadas en carpetas para empleados y clientes.

#### **Plantillas Principales:**
- **_LayoutCliente.cshtml:** Diseño para clientes.
- **_LayoutEmpleado.cshtml:** Diseño para empleados.
- **_LayoutInicio.cshtml:** Diseño para inicio de sesión.
- **DashboardCliente:** Dashboard para clientes.
- **DashboardEmpleado:** Dashboard para empleados.
- **Reservacion:** Página para reservar vehículos.
- **GestionReserva:** Gestión de reservas.
- **PagoAdmin:** Administración de pagos.
- **segurosAdmin:** Administración de seguros.
- **Index:** Página de inicio.

### 7. Configuración del Servidor

El proyecto usa **ASP.NET Core** con middleware para sesiones, autorización y rutas, además de **HTTPS** para seguridad.

#### **Configuración Principal:**

```csharp
var builder = WebApplication.CreateBuilder(args);

// Agregar servicios al contenedor de dependencias
builder.Services.AddControllersWithViews();
builder.Services.AddSession(); // Agregar soporte para sesiones

var app = builder.Build();

app.UseHttpsRedirection();
app.UseStaticFiles(); // Uso de archivos estáticos
app.UseRouting();
app.UseSession(); // Uso de sesiones
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Usuario}/{action=Login}/{id?}"
);

app.Run();
```

## Funcionalidades del Sistema

### 1. Registro y Autenticación
- Registro de clientes y empleados.
- Diferentes accesos según rol.

### 2. Gestión de Vehículos
- CRUD de vehículos.
- Filtros por marca, modelo, año, precio, estado y categoría.

### 3. Gestión de Reservas
- Reservas por períodos específicos.
- Confirmación y cancelación de reservas.
- Pagos y contratación de seguros.

### 4. Estadísticas
- Visualización de vehículos y clientes registrados.

### 5. Notificaciones y Alertas
- Uso de **SweetAlert** para notificaciones.

## Conclusiones

Este proyecto proporciona una solución completa para la gestión de una empresa de renta de vehículos. La estructura modular y escalable facilita su mantenimiento y expansión futura. El uso de **ASP.NET Core, Razor Pages y JavaScript** permite una interfaz fluida y funcional.
