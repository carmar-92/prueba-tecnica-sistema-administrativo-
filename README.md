# Prueba Técnica - Sistema de Facturación y Control de Inventario

Esta es una API RESTful desarrollada como prueba técnica para evaluar el dominio en el desarrollo de software backend. El sistema gestiona clientes, inventario de productos, un proceso de facturación transaccional y generación de reportes, todo bajo un entorno seguro.

## Tecnologías y Patrones Utilizados
* Framework:.NET 8 (C#)
* Base de Datos:MySQL
* Acceso a Datos: ADO.NET (Consultas parametrizadas para evitar Inyección SQL)
* Arquitectura: Arquitectura ordenada dividida en capas:
  + `PruebaTecnica.API` (Controladores, Middlewares, Seguridad)
  + `PruebaTecnica.Core` (Entidades, Interfaces)
  + `PruebaTecnica.Infrastructure` (Repositorios, Conexión a Base de Datos)
* Seguridad:Autenticación y Autorización mediante Tokens JWT.
* Documentación: Swagger (OpenAPI) configurado para soportar JWT.

## Módulos Implementados
1. Autenticación:Login seguro con JWT. Registro de intentos fallidos en bitácora.
2. Clientes:Crear, editar, listar y buscar (por nombre, identidad/RTN o correo).
3. Productos:Crear, editar, listar y desactivado lógico.
4. Facturación:Proceso seguro usando Transacciones SQL (`MySqlTransaction`). Calcula ISV (15%), guarda el detalle y descuenta el inventario automáticamente. Si algo falla, se revierte todo el proceso.
5. Reportes SQL:
   + Top 5 productos más vendidos.
   + Clientes con mayor facturación (Top 10).
   + Productos con inventario bajo (Stock < 5).
6. *Bitácora / Manejo de Errores:* Middleware global que captura cualquier excepción del sistema y la registra en la tabla `BitacoraErrores`, devolviendo un mensaje amigable al usuario (HTTP 500).

## Instrucciones de Ejecución (Local)

### 1. Base de Datos
1. Abre tu gestor de MySQL.
2. Ejecuta el script SQL proporcionado en este repositorio para crear la base de datos `PruebaTecnicaDB` y sus tablas con algunos datos de prueba.

### 2. Configuración de la API
1. Abre la solución en Visual Studio 2022.
2. Ve al proyecto `PruebaTecnica.API` y abre el archivo `appsettings.json`.
3. Modifica la sección `ConnectionStrings` con tu usuario y contraseña de MySQL local:
"ConnectionStrings": {
     "DefaultConnection": "Server=localhost;Database=PruebaTecnicaDB;Uid=root;Pwd=CONTRASEÑA_LOCAL_;"   }

### 3. Ejecutar y Probar API
1. Presiona F5 para compilar y ejecutar el proyecto. Se abrirá automáticamente la interfaz de Swagger en tu navegador.
2. Para probar los endpoints protegidos, primero debes autenticarte.
   + Credenciales de Prueba
     Utiliza el siguiente usuario en el endpoint POST /api/Auth/login para generar tu Token JWT:
      Correo: calomar.1992@gmail.com
      Contraseña: Carlos123

Nota: Una vez obtengas el token en la respuesta, copia en el botón de "Authorize" en la parte superior de Swagger, pega y haz clic en Authorize. Esto desbloqueará el resto de los módulos.

**Autor**: Carlos Eduardo Martínez Sosa - Ingeniero en Ciencias de la Computación

# Prueba Técnica - Sistema de Facturación y Control de Inventario
Esta es una solución de software integral desarrollada como prueba técnica para evaluar el dominio en el desarrollo. El sistema gestiona clientes, inventario de productos, un proceso de facturación transaccional y un panel de control dinámico, todo operando bajo una capa de seguridad.

## Tecnologías y Herramientas Utilizadas
### Backend (API Restful):
+ Framework: .NET 8 (C#)
+ Base de Datos: MySQL
+ Acceso a Datos: ADO.NET (Consultas parametrizadas para máxima eficiencia y prevención de Inyección SQL).
+ Seguridad: Autenticación y Autorización mediante Tokens JWT.
+ Documentación: Swagger (OpenAPI) configurado con soporte para JWT.

### Frontend (Cliente Web):
+ Framework: Blazor WebAssembly (.NET 8)
+ Diseño y UI: Bootstrap 5, CSS personalizado, íconos de Bootstrap.
+ Visualización de Datos: Chart.js integrado vía JSInterop.
+ Gestión de Estado: Blazored.LocalStorage para manejo persistente y seguro del JWT.

## Arquitectura del Sistema
El proyecto sigue los principios de Arquitectura Limpia y separación de responsabilidades, estructurado en las siguientes capas:

+ PruebaTecnica.API: Controladores, configuración de Middlewares, generación y validación de JWT.
+ PruebaTecnica.Core: Lógica de dominio, Entidades (Modelos) e Interfaces (Contratos).
+ PruebaTecnica.Infrastructure: Implementación de Repositorios, conexión a la base de datos MySQL y ejecución de consultas.
+ PruebaTecnica.Frontend: Aplicación cliente en Blazor WebAssembly que consume la API a través de servicios inyectados (HttpClient).

## Módulos Implementados
+ Autenticación y Seguridad: * Login seguro con JWT.
+ Registro de intentos fallidos en bitácora.
+ La identidad del usuario para transacciones se extrae directamente de los claims del Token en el servidor, evitando vulnerabilidades por manipulación en el cliente.
+ Dashboard: * Gráfico dinámico de ventas mensuales.
+ KPIs en tiempo real: Top 5 productos más vendidos y Top 10 de mejores clientes.
+ Sistema de alertas tempranas para inventario bajo (Stock < 5).
+ Mantenimiento de Clientes: Crear, editar, listar y buscar registros.
+ Mantenimiento de Productos: Crear, editar, listar y desactivación (borrado lógico).
+ Proceso de Facturación: Experiencia de usuario dinámica en memoria (cálculos en tiempo real).
+ Lógica de negocios validada en el backend (verificación estricta de precios e inventario en base de datos).
+ Transacciones SQL ACID (MySqlTransaction): Guarda el encabezado de la factura, el detalle, calcula el ISV (15%) y descuenta el inventario simultáneamente. Si un paso falla, se ejecuta un Rollback automático garantizando la integridad de los datos.
+Bitácora / Manejo de Errores: Middleware global en la API que captura excepciones no controladas, las registra en la tabla BitacoraErrores y devuelve un mensaje seguro y amigable al usuario (HTTP 500).

## Instrucciones de Ejecución (Local)
### Base de Datos
+ Abre tu gestor de base de datos MySQL.
+ Ejecuta el script SQL proporcionado en el repositorio para crear la base de datos PruebaTecnicaDB junto con sus tablas y datos de prueba iniciales.

### Configuración de la API
+ Abre la solución en Visual Studio 2022.
+ Expande el proyecto PruebaTecnica.API y abre el archivo appsettings.json.
+ Modifica la cadena de conexión con tus credenciales locales de MySQL:

JSON
"ConnectionStrings": {
  "DefaultConnection": "Server=localhost;Database=PruebaTecnicaDB;Uid=root;Pwd=TU_CONTRASEÑA_LOCAL;"
}
### Configuración del Entorno de Ejecución (Visual Studio)
Para que el sistema funcione correctamente, la API y el Frontend deben ejecutarse de manera simultánea:

+ Haz clic derecho sobre la Solución PruebaTecnica en el Explorador de Soluciones.
+ Selecciona Configurar proyectos de inicio.
+ Selecciona la opción Proyectos de inicio múltiples.
+ Configura tanto PruebaTecnica.API como PruebaTecnica.Frontend con la acción Iniciar.
+ Haz clic en Aplicar y Aceptar.

### Ejecutar y Probar
+ Presiona F5 para compilar y lanzar el sistema. Se abrirán dos pestañas en tu navegador: Swagger (Backend) y la Interfaz Web (Frontend).
+ Para acceder al sistema desde el Frontend, utiliza las siguientes credenciales de prueba:
    * Correo: calomar.1992@gmail.com * 
    * Contraseña: Carlos123 *

** (Nota: Si prefieres interactuar directamente con la API mediante Swagger, asegúrate de realizar un POST a /api/Auth/login para obtener tu Token JWT, y pégalo en el botón "Authorize" en la parte superior derecha de la interfaz). **

Autor: Carlos Eduardo Martínez Sosa - Ingeniero en Ciencias de la Computación - Prueba Técnica - Sistema de Facturación y Control de Inventario