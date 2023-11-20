import 'dart:io';

import 'package:file_picker/file_picker.dart';

abstract class Coordenadas {
  double get latitude;
  double get longitude;
}

class GoogleMapsLatLng implements Coordenadas {
  final double latitude;
  final double longitude;

  GoogleMapsLatLng(this.latitude, this.longitude);
}

class LatLongLatLng implements Coordenadas {
  final double latitude;
  final double longitude;

  LatLongLatLng(this.latitude, this.longitude);
}

class GPXHelper {
  String generateGPXContent(List<Coordenadas> routePoints) {
    final StringBuffer gpxContent = StringBuffer();

    gpxContent
        .writeln('<?xml version="1.0" encoding="UTF-8" standalone="no" ?>');
    gpxContent.writeln(
        '<gpx version="1.1" xmlns="http://www.topografix.com/GPX/1/1">');

    for (Coordenadas point in routePoints) {
      gpxContent
          .writeln('  <wpt lat="${point.latitude}" lon="${point.longitude}">');
      gpxContent.writeln('    <name>Waypoint</name>');
      gpxContent.writeln('    <desc>Details about the waypoint</desc>');
      gpxContent.writeln('  </wpt>');
    }

    return gpxContent.toString();
  }

  Future<void> saveGPXToFile(List<Coordenadas> routePoints) async {
    try {
      // Solicitar al usuario que seleccione la carpeta de destino
      FilePickerResult? result = await FilePicker.platform.pickFiles();

      if (result == null) {
        print('La selección de la carpeta fue cancelada.');
        return;
      }

      // Mostrar al usuario las rutas seleccionadas
      print('Rutas seleccionadas:');
      for (String? path in result.paths) {
        if (path != null) {
          print(path);
        }
      }

      // Permitir al usuario seleccionar la ruta específica
      print('Seleccione la ruta donde desea guardar el archivo:');
      int selectedPathIndex = int.parse(stdin.readLineSync() ?? '0');

      // Verificar si el índice seleccionado es válido
      if (selectedPathIndex >= 0 && selectedPathIndex < result.paths.length) {
        // Construir ruta de archivo
        String fileName = await inputFileNameFromUser();
        String? selectedPath = result.paths[selectedPathIndex];

        // Verificar si la ruta seleccionada no es nula
        if (selectedPath != null) {
          String filePath = '$selectedPath/$fileName';

          // Guardar archivo
          File file = File(filePath);
          await file.writeAsString(generateGPXContent(routePoints));

          print('Archivo GPX guardado en: $filePath');
        } else {
          print('La ruta seleccionada es nula.');
        }
      } else {
        print('Índice seleccionado no válido.');
      }
    } catch (e) {
      print('Error guardando archivo GPX: $e');
    }
  }

  Future<String> inputFileNameFromUser() async {
    print('Ingresa el nombre del archivo GPX:');
    String? name = stdin.readLineSync();
    return name ?? 'straviaTEC_route.gpx';
  }
}
