# Modo de uso

## LLamadas a api con ingreso de ip
curl https://localhost:5001/api/ipinfo/5.6.7.8 -UseBasicParsing -o result.json

### IP BR 
curl https://localhost:5001/api/ipinfo/179.183.250.219 -UseBasicParsing -o resultBR.json

### IP ES 
curl https://localhost:5001/api/ipinfo/213.192.202.121 -UseBasicParsing -o resultES.json

### IP UY 
curl https://localhost:5001/api/ipinfo/200.40.96.160 -UseBasicParsing -o resultUY.json

### IP USA 
curl https://localhost:5001/api/ipinfo/72.229.28.185 -UseBasicParsing -o resultUSA.json

## LLamadas a api de estadisticas
curl https://localhost:5001/api/ipinfo/ -UseBasicParsing -o statistics.json
