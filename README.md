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

