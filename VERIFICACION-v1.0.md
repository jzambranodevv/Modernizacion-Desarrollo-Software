# Verificación — Inventario v1.0

## Resultado ejecutado

- Rama: feature/version-1.0. Sin commits, push, Pull Request ni etiqueta.
- Solución estándar: SistemaInventario.sln; aplicación Windows Forms y proyecto PruebasInventario, ambos para .NET Framework 4.7.2.
- Targeting pack 4.7.2 encontrado en el equipo.
- Restauración de MSTest.TestFramework y MSTest.TestAdapter 2.2.10 completada.
- Compilación Release de la solución con MSBuild de Visual Studio Insiders: correcta.
- Compilación Debug del estado final: correcta, sin errores ni advertencias de MSBuild.
- VSTest 18.11.0: 22 pruebas aprobadas, 0 fallidas, tanto en Release como en Debug. Debug incluye las comprobaciones adicionales finales de conservación de productos.
- Evidencia local: TestResults/inventario.trx y TestResults/inventario-debug.trx (archivos de ejecución ignorados por Git).
- Apertura del ejecutable Release: correcta. Revisión visual: campos, botones y mensajes completos y legibles.
- Se corrigió una advertencia MSB4121 causada por un salto de línea faltante en la configuración Debug de la solución; la compilación posterior ya no la presenta.

## Cobertura y pendientes

| Criterios | Estado |
|---|---|
| CA01 | Registro con datos tipados aprobado; pendientes las dos entradas textuales 15,50 y 15.50 en el formulario, cada una desde Vacío. |
| CA02–CA12 y CA15 | Reglas del controlador aprobadas mediante 21 ejecuciones, con estado inicial restaurado por caso y variante; CA01 aporta la ejecución restante. |
| CA13–CA14 | Pendientes de ejecución interactiva en el formulario; no se consideran aprobados. |
| Mensajes y selección por código | Revisados en código; pendiente comprobación interactiva de las operaciones. |
| Designer de Visual Studio | Archivo parcial estándar generado y compilado; pendiente abrirlo en el diseñador visual y comprobar edición/guardado. |
| GitHub Actions | Workflow creado; ejecución remota pendiente. |

El control de Windows devolvió repetidamente «window bounds changed; call get_window_state before using this window» al intentar editar el primer campo, incluso después de actualizar el estado. No se llegó a introducir ni registrar datos mediante la interfaz. La apertura y la captura visual sí se realizaron.

La restauración inicial falló con NU1301 por resolución de api.nuget.org en el entorno restringido; el reintento autorizado fuera de esa restricción completó la restauración. No es un fallo pendiente del proyecto.

## Reproducción local

Abrir SistemaInventario.sln en Visual Studio Insiders con la carga de trabajo de escritorio .NET y el Developer Pack 4.7.2. Establecer SistemaInventario como proyecto de inicio si fuera necesario, restaurar NuGet y compilar.

Desde Developer PowerShell de Visual Studio:

    MSBuild.exe SistemaInventario.sln /restore /p:Configuration=Release /v:minimal

Ejecutar VSTest de esa instalación de Visual Studio:

    vstest.console.exe PruebasInventario\bin\Release\PruebasInventario.dll /TestAdapterPath:packages\mstest.testadapter\2.2.10\build\_common /Logger:trx /ResultsDirectory:TestResults

Comprobar que se descubren 22 pruebas, todas aprobadas; conservar el TRX. El workflow usa la misma compilación y ejecución, verifica el targeting pack, instala el Developer Pack de Microsoft si falta y falla ante errores o ausencia de pruebas.

## Revisión manual pendiente

1. Ejecutar CA01 desde Vacío, cerrando y abriendo la aplicación entre las variantes de coma y punto decimal.
2. Para cada variante de CA13 y CA14, iniciar una sesión nueva y registrar Base como indica spec.md. Introducir un único dato inválido por variante, tanto al registrar P003 como al modificar P001. Comprobar el mensaje y registrar el resultado; no marcar aprobado sin comprobar la conservación de los datos.
3. Verificar los mensajes de éxito/error, la entrada del código para modificar/eliminar y que el código original no cambie.
4. Abrir frmProductos en el diseñador visual y verificar su edición y guardado.
5. Ejecutar el workflow cuando se autorice publicar los cambios. Conservar el artefacto TRX y revisar los resultados.

La interfaz admite entrada directa del código, sin listados, filtros ni búsqueda. Los datos se mantienen únicamente en memoria y se pierden al cerrar.

## Estado SDD

T01–T06 completadas. T07 parcial por las comprobaciones interactivas pendientes. T08 parcial: configuración lista, sin ejecución remota. T09 no realizada por instrucción del usuario.

requirements.md, spec.md y plan.md conservan sus comportamientos; tasks.md refleja el avance real.