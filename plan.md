# Plan técnico — Sistema de Inventario v1.0

**Base:** `requirements.md` y `spec.md`. **Estado:** planificación, sin implementación.

## Tecnología

* C#, Windows Forms y .NET Framework 4.7.2, en Visual Studio.
* Productos almacenados en memoria (`List<Producto>`); no usar base de datos ni paquetes visuales externos.
* Separación **Entidades–Controlador–Formularios**.

## Estructura

* `SistemaInventario/Entidades/Producto.cs`: código, nombre, precio (`decimal`), cantidad y stock mínimo (`int`).
* `SistemaInventario/Controlador/TLista.cs`: lista de productos y operaciones de registrar, modificar y eliminar. Centraliza validaciones; indica éxito o fallo y motivo. La interfaz no debe duplicar reglas del negocio.
* `SistemaInventario/Formularios/frmProductos.cs` y su archivo Designer: formulario para ingresar datos y ejecutar las tres operaciones. Puede tener un selector sencillo de códigos existentes para modificar/eliminar; **no** implementar aún consulta ni búsqueda avanzada.
* `SistemaInventario/Program.cs` y archivos de proyecto/solución necesarios para abrir y ejecutar en Visual Studio.
* `PruebasInventario/`: proyecto de pruebas automatizadas compatible con .NET Framework 4.7.2, destinado a verificar las reglas de negocio y los criterios CA01–CA09 sin depender de la interfaz gráfica. Si es necesario, la lógica podrá organizarse en una biblioteca independiente para facilitar las pruebas.

## Interfaz

* Un formulario principal profesional y sobrio: título, campos bien alineados, botones Registrar/Modificar/Eliminar, mensajes claros y estados de validación.
* Controles nativos de Windows Forms y Designer compatible. No añadir panel de indicadores, movimientos de inventario ni otras funciones previstas para v1.1/v1.2.

## Decisiones de comportamiento

* Código y nombre se guardan sin espacios al inicio o al final. Los códigos se comparan sin distinguir mayúsculas/minúsculas.
* El código no puede modificarse en v1.0.
* En un fallo, ninguna operación debe alterar los datos existentes.
* Datos en memoria: se pierden al cerrar la aplicación; es una limitación declarada del prototipo.
* La interfaz puede mostrar información mínima para elegir el producto a modificar o eliminar; no equivale al módulo de consulta de v1.1.

## Verificación y automatización

El sistema se verificará mediante pruebas basadas en los criterios de aceptación CA01–CA09, prestando especial atención a las validaciones y operaciones sobre productos inexistentes.

Para la automatización se utilizará GitHub Actions con un ejecutor Windows, MSBuild para compilar la solución y un proyecto de pruebas automatizadas. El pipeline deberá indicar un error cuando falle la compilación o alguna prueba.

