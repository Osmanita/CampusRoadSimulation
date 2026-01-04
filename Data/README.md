\# Source Data



This directory contains the processed geospatial data used for terrain generation in Unity.



\## DEM (Heightmap)



The following files represent the same elevation data in different formats, each serving a specific purpose:



\- \*\*campus\_heightmap.raw\*\*

&nbsp; - Format: Grayscale RAW

&nbsp; - Purpose: Direct input for Unity terrain heightmap

&nbsp; - Source: USGS DEM

&nbsp; - Processing: Cropped and processed using QGIS and GDAL



\- \*\*campus\_heightmap.png\*\*

&nbsp; - Format: Grayscale PNG

&nbsp; - Purpose: Human-readable visualization and validation of elevation data



\- \*\*campus\_heightmap.hdr\*\*

&nbsp; - Format: HDR

&nbsp; - Purpose: Preserves higher precision elevation values during intermediate processing



\## Satellite Imagery



\- \*\*KAMPUS.png\*\*

&nbsp; - Source: Bing Maps

&nbsp; - Processing: Cropped to campus boundaries using polygon masking in QGIS

&nbsp; - Purpose: Reference texture for terrain texturing in Unity



All data is stored here to ensure reproducibility and transparency of the terrain generation pipeline.



