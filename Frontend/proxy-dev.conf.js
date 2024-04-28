const defaultTarget = 'http://localhost:5292';

module.exports = [
  {
    context: ['/api/**'],
    target: defaultTarget,
    changeOrigin: true
  }
]
