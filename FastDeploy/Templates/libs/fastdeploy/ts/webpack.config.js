const path = require('path');

module.exports = {
  entry: './src/fastdeploy.ts',
  devtool: 'source-map',
  module: {
    rules: [
      {
        test: /\.tsx?$/,
        use: 'ts-loader',
        exclude: /node_modules/
      },
    ],
  },
  resolve: {
    extensions: ['.tsx', '.ts', '.js'],
    alias: {
      controls: path.resolve(__dirname, './src/controls/fd-controls.ts'),
      shared: path.resolve(__dirname, './src/shared/fd-shared.ts')
    }
  },
  output: {
    library: {
      name: 'FastDeploy',
      type: 'var'
    },
    filename: 'fastdeploy.min.js',
    path: path.resolve(__dirname, './dist'),
  },
  mode: 'development'
};