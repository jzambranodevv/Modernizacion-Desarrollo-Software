# Plan técnico — Sistema de Inventario v1.0

**Base:** `requirements.md` y `spec.md`. **Estado:** planificación, sin implementación.

## Tecnología

* C#, Windows Forms y .NET Framework 4.7.2, en Visual Studio.
* Productos almacenados en memoria (`List<Producto>`); no usar base de datos ni paquetes visuales externos.
* Separación **Entidades–Controlador–Formularios**.

## Estructura

* `SistemaInventario/Entidades/Producto.cs`: código, nombre, precio (`decimal`), cantidad y stock mínimo (`int`).
* `SistemaInventario/Controlador/TLista.cs`: lista de productos y operaciones de registrar, modificar y eliminar. Centraliza validaciones; indica éxito o fallo y motivo. La interfaz no debe duplicar reglas del negocio.
* `SistemaInventario/Formularios/frmProductos.cs` y su archivo Designer: formulario para ingresar datos y ejecutar las tres operaciones. Convierte las entradas a `decimal` e `int` según `spec.md` y explica los errores de conversión sin cambiar datos. `TLista` valida las reglas de negocio; el formulario no las duplica. Solo permite seleccionar por código para modificar/eliminar, mediante un selector sencillo de códigos existentes o la entrada del código; no incluye listados generales de productos, filtros ni búsquedas avanzadas.
* `SistemaInventario/Program.cs` y archivos de proyecto/solución necesarios para abrir y ejecutar en Visual Studio.
* `PruebasInventario/`: proyecto de pruebas dirigido a .NET Framework 4.7.2 con MSTest V2, usando `MSTest.TestFramework` y `MSTest.TestAdapter` en la misma versión 2.2.10. Verifica entidad y controlador sin abrir formularios ni añadir clases o bibliotecas innecesarias.

## Interfaz

* Un formulario principal profesional y sobrio: título, campos bien alineados, botones Registrar/Modificar/Eliminar, mensajes claros y estados de validación.
* Controles nativos de Windows Forms y Designer compatible. No añadir panel de indicadores, movimientos de inventario ni otras funciones previstas para v1.1/v1.2.

## Decisiones de comportamiento

* Código y nombre se guardan sin espacios al inicio o al final. Los códigos se comparan sin distinguir mayúsculas/minúsculas.
* El código no puede modificarse en v1.0.
* En un fallo, ninguna operación debe alterar los datos existentes.
* Datos en memoria: se pierden al cerrar la aplicación; es una limitación declarada del prototipo.
* La selección por código sirve únicamente para modificar o eliminar; no habilita consultas generales, filtros ni búsquedas avanzadas de v1.1.

## Verificación y automatización

Las pruebas automatizadas del controlador cubren CA01–CA12 y CA15 con datos tipados, estados iniciales independientes y comprobación de que los demás productos se conservan. CA13–CA14 se verifican manualmente en el formulario, porque comprueban la conversión de texto antes de invocar al controlador. La revisión manual también cubre los mensajes, el precio con coma o punto de CA01 y la selección por código. Se registran resultados y evidencias de CA01–CA15.

GitHub Actions usará un ejecutor `windows-2022`: restaurar paquetes NuGet, compilar la solución en Release con MSBuild de Visual Studio y ejecutar la DLL de pruebas con `vstest.console.exe`, indicando la ruta del adaptador MSTest V2 restaurado. Se localizarán MSBuild y VSTest mediante la instalación de Visual Studio. Se guardarán resultados TRX como evidencia y el pipeline fallará si falla la compilación, alguna prueba o no se descubre ninguna prueba.

Antes de compilar, comprobar la disponibilidad del targeting pack de .NET Framework 4.7.2 (ensamblados de referencia); si falta, instalar el Developer Pack 4.7.2 desde Microsoft y verificarlo de nuevo. El runtime por sí solo no basta. Aplicar la misma comprobación en Visual Studio local. La integración requiere pruebas automáticas aprobadas, revisión manual registrada y un Pull Request obligatorio.

