/*
 * FINMER - Interactive Text Adventure
 * Copyright (C) 2019-2023 Nuntis the Wolf.
 *
 * Licensed under the GNU General Public License v3.0 (GPL3). See LICENSE.md for details.
 * SPDX-License-Identifier: GPL-3.0-only
 */

import React from 'react';
import styles from './styles.module.css';

export default function CaptionedImage({src, alt, caption}) {
  return (
    <figure>
      <img src={src.default} alt={alt} />
      <figcaption className={styles.imageCaption}>
	  {caption}
      </figcaption>
    </figure>
  );
}
